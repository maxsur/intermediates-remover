using System;

namespace IntermediatesRemoverCommon
{
    public sealed class FindProgressEventArgs : EventArgs
    {
        public string Folder { get; }

        public FindProgressEventArgs(string folder)
        {
            Folder = folder;
        }
    }
}