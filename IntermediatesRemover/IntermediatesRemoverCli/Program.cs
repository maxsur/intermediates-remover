﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using CommandLine;
using IntermediatesRemoverCommon;
using JetBrains.Annotations;

namespace IntermediatesRemoverCli
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            CliOptions options = new CliOptions();
            Parser.Default.ParseArguments<CliOptions>(args).WithParsed(opts => options = opts);

            Console.WriteLine($"Root folder: {options.RootFolderName}");

            // Should be case sensitive
            const string defaultFolderNames = "bin,obj,Debug,Release,DebugUnitTests,TestResults,lut";
            
            Console.WriteLine($"Hello '{defaultFolderNames}'! and {args}");

            using var source = new CancellationTokenSource();

            ICollection<string> foundFolderNames = new Collection<string>();

            var folders = new Folders(source.Token);
            folders.FindProgressChanged += (sender, eventArgs) => foundFolderNames.Add(eventArgs.Folder);
            folders.FindFolders(options.RootFolderName, defaultFolderNames.Split(','));

            var foundFolders = foundFolderNames.Select(folderName => new Folder(folderName));
            foreach (var folder in foundFolders) folder.Delete();

            if (ConsoleMode.IsConsoleWillBeDestroyedAtTheEnd)
            {
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
            }
        }
    }

    /// <summary> CLI options. </summary>
    [UsedImplicitly]
    public sealed class CliOptions
    {
        [Option('r', "root", Required = true, HelpText = "Root folder with Visual Studio projects.")]
        public string RootFolderName { get; set; }
    }
}
