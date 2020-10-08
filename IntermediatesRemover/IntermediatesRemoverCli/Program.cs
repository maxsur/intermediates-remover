using System;

namespace IntermediatesRemoverCli
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string defaultFolderNames = "bin,obj,Debug,Release,DebugUnitTests,TestResults,lut";
            Console.WriteLine($"Hello '{defaultFolderNames}'!");
        }
    }
}
