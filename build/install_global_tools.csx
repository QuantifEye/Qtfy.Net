using System;
using System.Diagnostics;

var tools = new[]
{
    "coverlet.console",
    "dotnet-reportgenerator-globaltool"
};

foreach (var tool in tools)
{
    Console.WriteLine($"Updating {tool}...");
    if (!TryRun("dotnet", "tool", "update", "-g", tool))
    {
        Console.WriteLine($"Installing {tool}...");
        Run("dotnet", "tool", "install", "-g", tool);
    }
}

static bool TryRun(string fileName, params string[] args)
{
    try
    {
        Run(fileName, args);
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
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
