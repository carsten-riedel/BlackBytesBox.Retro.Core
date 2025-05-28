using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BlackBytesBox.Retro.Core
{
    public sealed class ConsoleHelper
    {
        private ConsoleHelper() { }  // prevent instantiation

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        private static IntPtr _foundHandle;
        private static uint _currentPid;

        /// <summary>
        /// Retrieves the HWND of the console window belonging to the current process.
        /// </summary>
        /// <remarks>
        /// &lt;GetConsoleWindow() isn’t available on NT 4.0&gt;, so we enumerate all top-level windows
        /// and match the process ID to find our console’s “ConsoleWindowClass”.
        /// </remarks>
        /// <returns>
        /// The window handle (HWND) of the console, or IntPtr.Zero if not found.
        /// </returns>
        /// <example>
        /// &lt;code&gt;
        /// // make sure a console exists
        /// AllocConsole();
        ///
        /// // then get its window handle:
        /// IntPtr hwnd = ConsoleHelper.GetConsoleWindowHandle();
        /// if (hwnd != IntPtr.Zero) {
        ///     // e.g. MoveWindow(hwnd, x, y, w, h, true);
        /// }
        /// &lt;/code&gt;
        /// </example>
        public static IntPtr GetConsoleWindowHandle()
        {
            _foundHandle = IntPtr.Zero;
            Process proc = Process.GetCurrentProcess();
            _currentPid = (uint)proc.Id;

            EnumWindows(new EnumWindowsProc(EnumWindowsCallback), IntPtr.Zero);
            return _foundHandle;
        }

        private static bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            uint pid;
            GetWindowThreadProcessId(hWnd, out pid);
            if (pid == _currentPid)
            {
                StringBuilder cls = new StringBuilder(256);
                GetClassName(hWnd, cls, cls.Capacity);
                if (cls.ToString() == "ConsoleWindowClass")
                {
                    _foundHandle = hWnd;
                    return false;   // stop enumeration
                }
            }
            return true;            // continue
        }

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
    }

}
