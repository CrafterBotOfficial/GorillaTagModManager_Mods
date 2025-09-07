var gamePath = Argument<string>("gamepath", "/home/crafterbot/.local/share/Steam/steamapps/common/Gorilla Tag/");

const string outputDLL = "ComputerInterface.dll";

Task("BuildMod")
    .Does(() =>
{
    Information("Building project...");
    string csproj = "./ComputerInterface/ComputerInterface.csproj";

    string dumbyPdbFilePath = "ComputerInterface/bin/Debug/netstandard2.1/ComputerInterface.pdb";
    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dumbyPdbFilePath));
    System.IO.File.Create(dumbyPdbFilePath).Dispose();

    Information("Patching csproj");
    System.IO.File.WriteAllText(csproj, System.IO.File.ReadAllText(csproj).Replace("Core Mods\\", "").Replace("\\Bepinject\\", "\\Bepinject-Auros\\")); // why is it built like this -_-

    var settings = new DotNetBuildSettings 
    { 
        Configuration = "Release", 
        ArgumentCustomization = args => args
            .Append($"/p:GamePath=\"{gamePath}\"")
    };
    DotNetBuild(csproj, settings);
});

RunTarget("BuildMod");
