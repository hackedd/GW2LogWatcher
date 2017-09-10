using System.Diagnostics;
using System.IO;

namespace GW2LogWatcher
{
    public class RaidHeroesHandler : LogFileHandler
    {
        public static string RaidHeroesLocation { get; set; }

        public override LogFileHandlerResults Handle(string filename)
        {
            Debug.Assert(RaidHeroesLocation != null, "RaidHeroesLocation != null");
            Debug.Assert(filename != null, "Filename != null");

            var directory = Path.GetDirectoryName(filename);

            var process = new Process
            {
                StartInfo =
                {
                    FileName = RaidHeroesLocation,
                    Arguments = "\"" + filename + "\"",
                    WorkingDirectory = directory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
            var files = Directory.GetFiles(directory, Path.GetFileNameWithoutExtension(filename) + "_*.html");
            var outputFile = files.Length == 1 ? files[0] : null;

            if (process.ExitCode != 0 || outputFile == null || outputFile.Contains("_err"))
                return new LogFileHandlerResults {Success = false, Output = output};

            return new LogFileHandlerResults { Success = true, Output = outputFile };
        }
    }
}
