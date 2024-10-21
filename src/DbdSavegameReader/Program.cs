using System.CommandLine;
using DbdSavegameReader.Lib.Serializer;

var inputArgument = new Argument<string>("input", "The path to the savegame file.");
var outputArgument = new Argument<string>("output", () => "save.json", "The path to the output file.");
var rootCommand = new RootCommand("Converts dead by daylight savegame files to json.");
rootCommand.AddArgument(inputArgument);
rootCommand.AddArgument(outputArgument);

rootCommand.SetHandler((input, output) =>
{
    var data = File.ReadAllText(input);
    var json = DbdSerializer.ReadJson(data);
    File.WriteAllText(output, json);
}, inputArgument, outputArgument);

return rootCommand.Invoke(args);