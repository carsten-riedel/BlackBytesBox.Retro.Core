using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System;
using BlackBytesBox.Retro.Core.Interop;
using System.Linq;
using BlackBytesBox.Retro.Core.Extensions;

namespace BlackBytesBox.Retro.Core.ConTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NativeMethods.ConsoleManager.EnsureConsole();
            NativeMethods.ConsoleManager.DisableCloseButton();
            NativeMethods.ConsoleManager.DisableQuickEdit();
            NativeMethods.ConsoleManager.SetFont(NativeMethods.ConsoleManager.ConsoleFont.LucidaConsole);
            NativeMethods.ConsoleManager.EnableUtf8();

            // 2) Compute how much to offset so the *client area* lands at (0,0)
            var deco = NativeMethods.WindowManager.GetWindowDecorations();

            //NativeMethods.ConsoleManager.MoveAndResize(0,0, 1000, 500);
            // 3) Move the console window so its client‐area is flush with the top-left

            NativeMethods.ConsoleManager.MoveAndResize(x: 0 - deco.BorderWidth, 0);

            Console.WriteLine("Unicode ✔ Ω 漢 😊");
            Console.WriteLine("Hello, World!");

            List<string> strings = new List<string>
            {
                "This is a test string.",
                "Another line of text.",
                "Yet another example.",
                "And one more for good measure."
            };

            strings.Where(s => s.Contains("test", StringComparison.OrdinalIgnoreCase))
                   .ToList()
                   .ForEach(Console.WriteLine);

            Thread.Sleep(5000);

            IntPtr desktopHwnd = NativeMethods.GetDesktopWindow();
            var semiTransBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
            using (var g = Graphics.FromHwnd(desktopHwnd))
            using (var pen = new Pen(Color.Red, 1))
            {
                g.FillRectangle(semiTransBrush, 0, 0, 300, 200);
            }

            IntPtr hDesk = IntPtr.Zero;
            try
            {
                // 1) Open the desktop receiving user input
                hDesk = NativeMethods.WindowManager.OpenCurrentInputDesktop();
                Console.WriteLine($"Opened input desktop: 0x{hDesk.ToInt64():X}");

                // 2) Enumerate windows on that desktop
                var windows = NativeMethods.WindowManager.EnumerateWindowsOnDesktop(hDesk);
                Console.WriteLine("Top-level windows on input desktop:");
                foreach (var hwnd in windows)
                {
                    Console.WriteLine($"  HWND = 0x{hwnd.ToInt64():X} {NativeMethods.WindowManager.GetWindowTitle(hwnd)}");
                }

            }
            catch (Win32Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message} (Win32 code {ex.NativeErrorCode})");
            }
            finally
            {
                // 3) Always close the desktop handle when done
                if (hDesk != IntPtr.Zero)
                {
                    NativeMethods.WindowManager.CloseInputDesktop(hDesk);
                    Console.WriteLine("Closed input desktop handle.");
                }
            }

            Console.WriteLine("Press the X in Windows Terminal—you won't exit!");
            Thread.Sleep(5000);
        }
    }
}
