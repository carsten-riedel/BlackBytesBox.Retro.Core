﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BlackBytesBox.Retro.Core.Interop
{
    public static partial class NativeMethods
    {
        private const uint ATTACH_PARENT_PROCESS = 0xFFFFFFFF;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);
    }
}