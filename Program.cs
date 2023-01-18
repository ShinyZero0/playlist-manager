using System;
using System.CommandLine;

namespace PlaylistManager
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("My playlist manager");
            var fileOption = new Option<FileInfo?>(
                name: "--file",
                description: "");
            var readCommand = new Command("read", "Display all the songs")
            {
                fileOption
            };
            readCommand.SetHandler((file) => 
                    { ReadFile(file!); },
                    fileOption);
            rootCommand.AddCommand(readCommand);
            return await rootCommand.InvokeAsync(args);
        }
        static void ReadFile(FileInfo file)
        {
            List<string> lines = File.ReadLines(file.FullName).ToList();
            foreach(string line in lines)
            {
                if (line.Contains("EXTINF"))
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
