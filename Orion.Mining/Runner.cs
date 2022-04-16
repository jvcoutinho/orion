using System.Diagnostics;

namespace Orion.Mining
{
    /// <summary>
    ///     Runs programs.
    /// </summary>
    public static class Runner
    {
        /// <summary>
        ///     Runs the program in the working directory with the provided arguments.
        ///     Suited for quick programs.
        /// </summary>
        /// <param name="programName">
        ///     Path of the program to run. If its path is in the PATH environment variable, its name is
        ///     enough.
        /// </param>
        /// <param name="workingDirectory">Directory to run this program.</param>
        /// <param name="arguments">Arguments to provide to the program.</param>
        /// <returns>The standard output, the standard error and the status code of the execution of the program.</returns>
        /// <exception cref="ProgramFailedException">The process didn't start.</exception>
        public static ProgramResult Run(string programName, string workingDirectory = ".", params string[] arguments)
        {
            ProcessStartInfo processStartInfo = new()
            {
                WorkingDirectory = workingDirectory,
                Arguments = string.Join(" ", arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = programName
            };

            var process = Process.Start(processStartInfo);
            if (process == null) throw new ProgramFailedException($"Process {processStartInfo} didn't start");

            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            return new ProgramResult(output, error, process.ExitCode);
        }
    }
}