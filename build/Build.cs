using System;
using System.IO;
using System.Linq;
using System.Net;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using Octokit;
using Octokit.Internal;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Project = Nuke.Common.ProjectModel.Project;

[GitHubActions(
    "test",
    GitHubActionsImage.UbuntuLatest,
    On = [GitHubActionsTrigger.PullRequest],
    InvokedTargets = [nameof(Test)],
    FetchDepth = 0
)]
[GitHubActions(
    "publish library",
    GitHubActionsImage.UbuntuLatest,
    On = [GitHubActionsTrigger.WorkflowDispatch],
    InvokedTargets = [nameof(Pack), nameof(Push)],
    ImportSecrets = [nameof(NugetApiKey)],
    FetchDepth = 0
)]
[GitHubActions(
    "publish app",
    GitHubActionsImage.UbuntuLatest,
    On = [GitHubActionsTrigger.WorkflowDispatch],
    InvokedTargets = [nameof(BuildApp), nameof(PublishApp)],
    ImportSecrets = [nameof(GithubToken)],
    FetchDepth = 0
)]
class Build : NukeBuild
{
    [Nuke.Common.Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [GitVersion] readonly GitVersion GitVersion;

    [Nuke.Common.Parameter("API Key for the NuGet server.")] [Secret] readonly string NugetApiKey;

    [Nuke.Common.Parameter("NuGet server URL.")] readonly string NugetSource = "https://api.nuget.org/v3/index.json";
    
    [Nuke.Common.Parameter("Github token")] [Secret] readonly string GithubToken;
    
    [Solution] readonly Solution Solution;
    
    GitHubActions GitHubActions => GitHubActions.Instance;
    
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Project LibProject => Solution.GetProject("DbdSavegameReader.Lib");
    Project AppProject => Solution.GetProject("DbdSavegameReader");

    Target Clean => d => d
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(path => path.DeleteDirectory());
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => d => d
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution)
            );
        });

    Target Compile => d => d
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .EnableNoRestore()
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
            );
        });

    Target Test => d => d
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .CombineWith(
                    from framework in LibProject.GetTargetFrameworks()
                    select new { framework }, (cs, v) => cs
                        .SetFramework(v.framework)
                )
            );
        });

    Target BuildApp => d => d
        .DependsOn(Clean, Test)
        .Requires(() => Configuration == Configuration.Release)
        .Produces(ArtifactsDirectory / "*.zip")
        .Executes(() =>
        {
            DotNetPublish(s => s
                .EnableNoRestore()
                .SetProject(AppProject)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.MajorMinorPatch)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetFramework("net8.0")
                .CombineWith(
                    from runtime in new[] {"win-x64", "linux-x64", "osx-x64"}
                    select new { runtime }, (cs, v) => cs
                        .SetRuntime(v.runtime)
                        .SetOutput(ArtifactsDirectory / "app" / v.runtime))
            );

            var directories = ArtifactsDirectory.GlobDirectories("app/*");
            foreach (var directory in directories)
            {
                directory.ZipTo(ArtifactsDirectory / $"DbdSavegameReader-{directory.Name}.zip");
            }
        });
    
    Target PublishApp => d => d
        .After(BuildApp)
        .Requires(() => Configuration == Configuration.Release)
        .Executes(async () =>
        {
            var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            if (string.IsNullOrEmpty(token))
            {
                Serilog.Log.Warning("GITHUB_TOKEN is not set");
                token = GitHubActions.Instance.Token;
            }

            if (string.IsNullOrEmpty(token))
            {
                Serilog.Log.Warning("GithubActions.Token is not set");
                token = GithubToken;
            }
            
            if(string.IsNullOrEmpty(token))
                Serilog.Log.Warning("Github secret is not set");
            
            var credentials = new Credentials(token);
            GitHubTasks.GitHubClient = new GitHubClient(
                new ProductHeaderValue(nameof(NukeBuild)),
                new InMemoryCredentialStore(credentials));
            var release = await GitHubTasks.GitHubClient.Repository.Release
                .Create("BorisGerretzen", "DbdSavegameReader", new NewRelease(GitVersion.MajorMinorPatch)
                {
                    Name = GitVersion.MajorMinorPatch,
                    Draft = true,
                    Body = "Release notes",
                    TargetCommitish = GitVersion.Sha
                });
            
            var zips = ArtifactsDirectory.GlobFiles("*.zip");
            foreach (var zip in zips)
            {
                await using var stream = File.OpenRead(zip);
                var releaseAsset = new ReleaseAssetUpload(zip.Name, "application/zip", stream, TimeSpan.FromMinutes(1));
                await GitHubTasks.GitHubClient.Repository.Release
                    .UploadAsset(release, releaseAsset);
            }
        });

    Target Pack => d => d
        .DependsOn(Clean, Test)
        .Requires(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            DotNetPack(s => s
                .EnableNoRestore()
                .EnableNoBuild()
                .SetProject(LibProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetProperty("PackageVersion", GitVersion.MajorMinorPatch)
            );
        });

    Target Push => d => d
        .After(Pack)
        .Executes(() =>
        {
            DotNetNuGetPush(s => s
                .SetSource(NugetSource)
                .SetApiKey(NugetApiKey)
                .CombineWith(ArtifactsDirectory.GlobFiles("*.nupkg"), (s, v) => s
                    .SetTargetPath(v)
                )
            );
        });

    public static int Main() => Execute<Build>(x => x.Test);
}