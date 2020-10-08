using System;
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
            
            Console.WriteLine($"Hello '{defaultFolderNames}'!");

            var source = new CancellationTokenSource();
            var folders = new Folders(source.Token);
            folders.FindProgressChanged += (sender, eventArgs) => Console.WriteLine(eventArgs.Folder);
            folders.FindFolders(defaultRootName, defaultFolderNames.Split(','));
        }
    }
}
