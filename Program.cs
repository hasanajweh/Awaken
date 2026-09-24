using System;
using System.Runtime.InteropServices;
using System.Threading;

internal static class Program
{
    private const string SingleInstanceMutexName = @"Local\DisplayKeep.ZeroInteraction";

    [Flags]
    private enum ExecutionState : uint
    {
        SystemRequired = 0x00000001,
        DisplayRequired = 0x00000002,
        Continuous = 0x80000000
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern ExecutionState SetThreadExecutionState(ExecutionState executionState);

    private static int Main()
    {
        bool ownsMutex;

        using (var singleInstance = new Mutex(
            initiallyOwned: true,
            name: SingleInstanceMutexName,
            createdNew: out ownsMutex))
        {
            if (!ownsMutex)
            {
                return 0;
            }

            var keepAwakeState = ExecutionState.Continuous
                | ExecutionState.SystemRequired
                | ExecutionState.DisplayRequired;

            if (SetThreadExecutionState(keepAwakeState) == 0)
            {
                int errorCode = Marshal.GetLastWin32Error();
                return errorCode == 0 ? 1 : errorCode;
            }

            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            finally
            {
                SetThreadExecutionState(ExecutionState.Continuous);
            }
        }

        return 0;
    }
}
