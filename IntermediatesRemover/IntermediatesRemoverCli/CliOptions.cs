using CommandLine;
using JetBrains.Annotations;

namespace IntermediatesRemoverCli
{
    /// <summary> CLI options. All members should be public. </summary>
    [UsedImplicitly]
    public sealed class CliOptions
    {
        [Option('r', "root", Required = true, HelpText = "Root folder with Visual Studio projects.")]
        public string RootFolderName { get; set; }
    }
}