#r "System.Xml.Linq"

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

const string Configuration = "Debug";
var projectNames = new[]
{
    "Qtfy.Numerics.Tests",
    "Qtfy.Numerics.Tests.BigRationals"
};

var root = Directory.GetCurrentDirectory();
var coverageDir = Path.Combine(root, "coverage");
var coverageFile = Path.Combine(coverageDir, "coverage.cobertura.xml");
var coverageSite = coverageFile + ".site";

ResetDirectory(coverageDir);

Run("dotnet", "clean");
Run("dotnet", "test", "-c", Configuration);

foreach (var projectName in projectNames)
{
    var projectDir = Path.Combine(root, "test", projectName);
    var projectFile = Path.Combine(projectDir, projectName + ".csproj");

    var targetFramework = GetTargetFramework(Path.Combine(root, "Directory.Build.props"), projectFile);
    if (string.IsNullOrWhiteSpace(targetFramework))
    {
        throw new InvalidOperationException(
            $"Unable to determine TargetFramework for coverage run ({projectName}).");
    }

    var testDll = Path.Combine(projectDir, "bin", Configuration, targetFramework, projectName + ".dll");
    var coverletArgs = new List<string>
    {
        testDll,
        "--target",
        "dotnet",
        "--targetargs",
        $"test {projectFile} --no-build -c {Configuration}",
        "--output",
        coverageFile,
        "--format",
        "cobertura"
    };

    if (File.Exists(coverageFile))
    {
        coverletArgs.Add("--merge-with");
        coverletArgs.Add(coverageFile);
    }

    Run(ResolveGlobalTool("coverlet"), coverletArgs.ToArray());
}

Run(
    ResolveGlobalTool("reportgenerator"),
    $"-reports:{coverageFile}",
    $"-targetdir:{coverageSite}.site",
    "-reporttypes:html");

static string GetTargetFramework(string propsPath, string projectPath)
{
    if (File.Exists(propsPath))
    {
        var doc = XDocument.Load(propsPath);
        var tfm = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "QtfyTargetFramework")?.Value;
        if (!string.IsNullOrWhiteSpace(tfm))
        {
            return tfm.Trim();
        }
    }

    if (File.Exists(projectPath))
    {
        var doc = XDocument.Load(projectPath);
        var tfm = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "TargetFramework")?.Value;
        if (!string.IsNullOrWhiteSpace(tfm))
        {
            return tfm.Trim();
        }

        var tfms = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "TargetFrameworks")?.Value;
        if (!string.IsNullOrWhiteSpace(tfms))
        {
            return tfms.Split(';')[0].Trim();
        }
    }

    return null;
}

static void ResetDirectory(string path)
{
    if (Directory.Exists(path))
    {
        Directory.Delete(path, true);
    }

    Directory.CreateDirectory(path);
}

static string ResolveGlobalTool(string toolName)
{
    var cliHome = Environment.GetEnvironmentVariable("DOTNET_CLI_HOME");
    if (string.IsNullOrWhiteSpace(cliHome))
    {
        cliHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }

    var toolsDir = Path.Combine(cliHome, ".dotnet", "tools");
    var toolFileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? toolName + ".exe" : toolName;
    var toolPath = Path.Combine(toolsDir, toolFileName);
    return File.Exists(toolPath) ? toolPath : toolName;
}

static void Run(string fileName, params string[] args)
{
    Console.WriteLine($"> {fileName} {string.Join(" ", args)}");
    var startInfo = new ProcessStartInfo
    {
        FileName = fileName,
        UseShellExecute = false
    };

    foreach (var arg in args)
    {
        startInfo.ArgumentList.Add(arg);
    }

    using var process = Process.Start(startInfo);
    if (process == null)
    {
        throw new InvalidOperationException($"Failed to start {fileName}");
    }

    process.WaitForExit();
    if (process.ExitCode != 0)
    {
        throw new InvalidOperationException($"{fileName} exited with code {process.ExitCode}");
    }
}
