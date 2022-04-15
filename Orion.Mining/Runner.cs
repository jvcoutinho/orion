using System.Diagnostics;

namespace Orion.Mining
{
    public static class Runner
    {
        public static (string, int) Run(string programName, string workingDirectory = ".", params string[] arguments)
        {
            return RunAsync(programName, workingDirectory, arguments).Result;
        }

        public static async Task<(string, int)> RunAsync(string programName, string workingDirectory = ".", params string[] arguments)
        {
            ProcessStartInfo processStartInfo = new()
            {
                WorkingDirectory = workingDirectory,
                Arguments = string.Join(" ", arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = programName,
            };

            var process = Process.Start(processStartInfo);
            if (process == null) throw new InvalidOperationException($"Could not run process {processStartInfo}");

            await process.WaitForExitAsync();

            return (process.StandardOutput.ReadToEnd(), process.ExitCode);
        }
    }
}