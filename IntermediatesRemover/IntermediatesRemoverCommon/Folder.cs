using System;
using System.IO;

namespace IntermediatesRemoverCommon
{
    public sealed class Folder
    {
        private readonly string _name;

        public Folder(string name)
        {
            _name = name;
        }

        public void Delete()
        {
            Console.Write($"Deleting {_name}...");
            try
            {
                Directory.Delete(_name, true); // recursive = true to remove directories, subdirectories, and files in path
                Console.WriteLine("Done.");
            }
            catch (IOException exception)
            {
                Console.WriteLine($"\n{exception}");
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine($"\n{exception}");
            }
        }
    }
}
