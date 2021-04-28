
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./") + Directory(configuration);
var netCoreVer = "netcoreapp2.2";
var netFrameworkVer = "net48";

var msBuildSettings = new MSBuildSettings()
    {
        Configuration = configuration,
        MaxCpuCount = System.Environment.ProcessorCount,
        Verbosity = Verbosity.Normal
    };

var dotnetSettings = new DotNetCorePublishSettings
    {
        Framework = netCoreVer,
        Configuration = "Release",
        OutputDirectory = buildDir + Directory("publish-" + netCoreVer)
    };

var cleanSettings = new DotNetCoreCleanSettings
    {
        Framework = dotnetSettings.Framework,
        Configuration = dotnetSettings.Configuration,
        OutputDirectory = dotnetSettings.OutputDirectory
    };

var licensefiles = new [] {
    "COPYING",
    "LICENSE.de4dot.txt",
    "LICENSE.dnlib.txt",
    "LICENSE.ICSharpCode.SharpZipLib.txt",
    "LICENSE.lzma.txt",
    "LICENSE.lzmat.txt",
    "LICENSE.QuickLZ.txt",
    "LICENSE.randomc.txt"
};
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./**/bin/" + configuration);
    CleanDirectories("./**/obj");
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./de4dot.netframework.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      // Use MSBuild
      MSBuild("./de4dot.netframework.sln", msBuildSettings);

});

Task("NetCoreBuild")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCorePublish("de4dot", dotnetSettings);

});

Task("NetCoreClean")
    .IsDependentOn("NetCoreBuild")
    .Does(() =>
{
    DotNetCoreClean("de4dot", cleanSettings);

});

//Task("Run-Unit-Tests")
    //.IsDependentOn("Build")
    //.Does(() =>
//{
    //NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        //NoResults = true
        //});
//});

Task("Zip-Files")
    .IsDependentOn("NetCoreClean")
    .Does(() =>
{
    // .NET Framework
    CreateDirectory(buildDir + Directory(netFrameworkVer) + Directory("LICENSE"));
    CopyFiles(licensefiles, buildDir + Directory(netFrameworkVer) + Directory("LICENSE"));
    DeleteFiles(GetFiles(MakeAbsolute(buildDir + Directory(netFrameworkVer)) + File("/*.pdb")));
    DeleteFiles(GetFiles(MakeAbsolute(buildDir + Directory(netFrameworkVer)) + File("/*.xml")));
    DeleteFiles(GetFiles(MakeAbsolute(buildDir + Directory(netFrameworkVer)) + File("/Test.Rename.*")));
    Zip(buildDir + Directory(netFrameworkVer), Directory(configuration) + File("de4dot-" + netFrameworkVer + ".zip"));

    // .NET Core
    CreateDirectory(buildDir + Directory("publish-" + netCoreVer) + Directory("LICENSE"));
    CopyFiles(licensefiles, buildDir + Directory("publish-" + netCoreVer) + Directory("LICENSE"));
    DeleteFiles(GetFiles(MakeAbsolute(dotnetSettings.OutputDirectory) + File("/*.pdb")));
    DeleteFiles(GetFiles(MakeAbsolute(dotnetSettings.OutputDirectory) + File("*.xml")));
    Zip(dotnetSettings.OutputDirectory, Directory(configuration) + File("de4dot-" + netCoreVer + ".zip"));
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Zip-Files");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
