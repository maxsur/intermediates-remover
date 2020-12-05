using System.Runtime.InteropServices;

namespace IntermediatesRemoverCli
{
    /// <summary>
    /// Represents mode of console running - from explorer or from cmd.
    /// https://stackoverflow.com/questions/1188658/how-can-a-c-sharp-windows-console-application-tell-if-it-is-run-interactively
    /// </summary>
    internal static class ConsoleMode
    {
        internal static bool IsConsoleWillBeDestroyedAtTheEnd
        {
            get
            {
                var processList = new uint[1];
                var processCount = GetConsoleProcessList(processList, 1);
                return processCount == 1;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetConsoleProcessList(uint[] processList, uint processCount);
    }
}