# DbdSavegameReader

[![NuGet](https://img.shields.io/nuget/v/DbdSavegameReader.svg)](https://www.nuget.org/packages/DbdSavegameReader/)

A simple C# library to read and decrypt Dead by Daylight savegame files.
Also contains a console application that does the same.

## Usage library

Here you can see how to read a savegame file and access the data.

```csharp
var fileContents = File.ReadAllText("profile.profjce");
var save = SavegameSerializer.Read(fileContents);
Console.WriteLine(save.UserId);
```

## Usage console application

Here you can see how to use the console application.

```
./DbdSavegameReader.exe <path to savegame file> [output file]
```