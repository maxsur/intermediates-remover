using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using IntermediatesRemoverCommon;

namespace IntermediatesRemoverCli
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string defaultRootName = @"C:\Projects";
            const string defaultFolderNames = "bin,obj,Debug,Release,DebugUnitTests,TestResults,lut";
            
            Console.WriteLine($"Hello '{defaultFolderNames}'! and {args}");

            using var source = new CancellationTokenSource();

            ICollection<string> foundFolderNames = new Collection<string>();

            var folders = new Folders(source.Token);
            folders.FindProgressChanged += (sender, eventArgs) => foundFolderNames.Add(eventArgs.Folder);
            folders.FindFolders(defaultRootName, defaultFolderNames.Split(','));


            var foundFolders = foundFolderNames.Select(folderName => new Folder(folderName));
            foreach (var folder in foundFolders) folder.Delete();
        }
    }
}
