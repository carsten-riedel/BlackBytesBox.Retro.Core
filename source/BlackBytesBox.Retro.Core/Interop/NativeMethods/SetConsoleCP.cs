﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BlackBytesBox.Retro.Core.Interop
{
    public static partial class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);
    }
}
