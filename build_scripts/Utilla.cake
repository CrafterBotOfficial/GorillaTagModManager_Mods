var gamePath = Argument<string>("gamepath", "/home/crafterbot/.local/share/Steam/steamapps/common/Gorilla Tag/");

const string outputDLL = "Utilla.dll";

Task("BuildMod")
    .Does(() =>
{
    Information("Building project...");

    string dumbyPdbFilePath = "Utilla/bin/Debug/netstandard2.1/Utilla.pdb";
    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dumbyPdbFilePath));
    System.IO.File.Create(dumbyPdbFilePath).Dispose();

    var settings = new DotNetBuildSettings 
    { 
        Configuration = "Release", 
        ArgumentCustomization = args => args
            .Append($"/p:GamePath=\"{gamePath}\"")
    };
    DotNetBuild("./Utilla/Utilla.csproj", settings);
});

RunTarget("BuildMod");
