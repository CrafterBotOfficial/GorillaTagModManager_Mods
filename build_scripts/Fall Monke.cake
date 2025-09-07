var gamePath = Argument<string>("gamepath", "/home/crafterbot/.local/share/Steam/steamapps/common/Gorilla Tag/");

const string outputDLL = "Fall Monke.dll";

Task("BuildMod")
    .Does(() =>
{
    Information("Building project...");
    EnvironmentVariable("GORILLATAG_PATH", gamePath);

    string dumbyPdbFilePath = "bin/Debug/netstandard2.1/FallMonke.pdb";
    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dumbyPdbFilePath));
    System.IO.File.Create(dumbyPdbFilePath).Dispose();

    var settingsRelease = new DotNetBuildSettings { Configuration = "Release", };

    DotNetBuild("./FallMonke.csproj", settingsRelease);
});

RunTarget("BuildMod");
