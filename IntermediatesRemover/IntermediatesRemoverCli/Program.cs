using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using CommandLine;
using IntermediatesRemoverCommon;

namespace IntermediatesRemoverCli
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                CliOptions options = new CliOptions(null);
                Parser.Default.ParseArguments<CliOptions>(args)
                    .WithParsed(opts => options = opts)
                    .WithNotParsed(errs => Exit(-1));

                if(options.RootFolderName == null) throw new InvalidOperationException("Root folder is not set");

                Console.WriteLine($"Root folder: {options.RootFolderName}");

                // Should be case sensitive
                const string defaultFolderNames = "bin,obj,Debug,Release,DebugUnitTests,TestResults,lut";

                using var source = new CancellationTokenSource();

                ICollection<string> foundFolderNames = new Collection<string>();

                var folders = new Folders(source.Token);
                folders.FindProgressChanged += (sender, eventArgs) => foundFolderNames.Add(eventArgs.Folder);
                folders.FindFolders(options.RootFolderName, defaultFolderNames.Split(','));

                var foundFolders = foundFolderNames.Select(folderName => new Folder(folderName));
                foreach (var folder in foundFolders) folder.Delete();

            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Console.WriteLine(e);
            }

            Exit(0);
        }

        private static void Exit(int exitCode)
        {
            if (ConsoleMode.IsConsoleWillBeDestroyedAtTheEnd)
            {
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
            }

            Environment.Exit(exitCode);
        }
    }
}
