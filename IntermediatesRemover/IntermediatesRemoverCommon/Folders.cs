using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace IntermediatesRemoverCommon
{
    /// <summary> Represents folder pathes found by filters. </summary>
    public sealed class Folders
    {
        private readonly CancellationToken _cancellationToken;

        public event EventHandler<FindProgressEventArgs>? FindProgressChanged;

        public Folders(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public void FindFolders(string startingPoint, IEnumerable<string> filters)
        {
            GetFolder(startingPoint, filters.ToList());
            //_bw.ReportProgress(100, progress);
        }

        private void GetFolder(string root, ICollection<string> filters)
        {
            var dirs = Directory.EnumerateDirectories(root, "*", SearchOption.TopDirectoryOnly);
            foreach (var strDir in dirs)
            {
                var found = false;

                // Get last part of path
                var iPos = strDir.LastIndexOf(Path.DirectorySeparatorChar);
                var strLastPart = strDir.Substring(iPos + 1);

                // Compare with filters
                foreach (var strFilter in filters)
                {
                    _cancellationToken.ThrowIfCancellationRequested();

                    if (strLastPart == strFilter)
                    {
                        OnFindProgressChanged(new FindProgressEventArgs(strDir));
                        found = true;
                    }
                }

                // Stop recurse subdirectories immidiately after first match. We do not need deper checks.
                if (!found) GetFolder(strDir, filters);
            }
        }

        private void OnFindProgressChanged(FindProgressEventArgs e) => FindProgressChanged?.Invoke(this, e);
    }
}
