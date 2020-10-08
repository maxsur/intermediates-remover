using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

            ICollection<string> foundFolders = new Collection<string>();

            var folders = new Folders(source.Token);
            folders.FindProgressChanged += (sender, eventArgs) =>
            {
                foundFolders.Add(eventArgs.Folder);
                //Console.WriteLine(eventArgs.Folder);
            };
            folders.FindFolders(defaultRootName, defaultFolderNames.Split(','));


            foreach (var folderName in foundFolders)
            {
                Console.Write($"Deleting {folderName}...");

                try
                {
                    Directory.Delete(folderName, true); // recursive = true to remove directories, subdirectories, and files in path
                    Console.WriteLine("Done.");
                }
                catch (IOException exception)
                {
                    Console.WriteLine($"\n{exception}");
                }
                catch(UnauthorizedAccessException exception)
                {
                    Console.WriteLine($"\n{exception}");
                }
            }
        }
    }
}
