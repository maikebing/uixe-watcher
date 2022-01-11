using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Uixe.Watcher.WinFormsLifetime
{
    public class ConsoleWindow
    {
        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        private struct COORD
        {
            public short X;
            public short Y;
        };

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleScreenBufferSize(
          IntPtr hConsoleOutput,
          COORD size
        );

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(
                IntPtr hWnd,
                int X,
                int Y,
                int nWidth,
                int nHeight,
                bool bRepaint
            );
 

        public static void Hide()
        {
            FreeConsole();
        }

        public static void Show( int bufferWidth = -1, bool breakRedirection = true, int bufferHeight = -1, int screenNum = -1 /*-1 = Any but primary*/)
        {
            AllocConsole();
            IntPtr stdOut = InvalidHandleValue, stdIn, stdErr;
            if (breakRedirection)
                UnredirectConsole(out stdOut, out stdIn, out stdErr);
            var outStream = Console.OpenStandardOutput() as Stream;
            var errStream = Console.OpenStandardError() as Stream;
            Encoding encoding = System.Text.Encoding.GetEncoding(936);
            StreamWriter standardOutput = new StreamWriter(outStream, encoding), standardError = new StreamWriter(errStream, encoding);
            System.Windows.Forms.Screen screen = null;
            try
            {
                if (screenNum < 0)
                    screen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();
                else
                    screen = System.Windows.Forms.Screen.AllScreens[Math.Min(screenNum, Screen.AllScreens.Count() - 1)];
            }
            catch (Exception )
            {
            }
            if (bufferWidth == -1)
            {
                if (screen == null)
                    bufferWidth = 180;
                else
                {
                    bufferWidth = screen.WorkingArea.Width / 10;
                    if (bufferWidth > 15)
                        bufferWidth -= 5;
                    else
                        bufferWidth = 10;
                }
            }
            try
            {
                standardOutput.AutoFlush = true;
                standardError.AutoFlush = true;
                Console.SetOut(standardOutput);
                Console.SetError(standardError);
                if (breakRedirection)
                {
                    var coord = new COORD();
                    coord.X = (short)bufferWidth;
                    coord.Y = (short)bufferHeight;
                    SetConsoleScreenBufferSize(stdOut, coord);
                }
                else
                {
                    Console.SetBufferSize(bufferWidth, bufferHeight);
                }
            }
            catch (Exception e) // Could be redirected
            {
                Debug.WriteLine(e.ToString());
            }
            try
            {
                if (screen != null)
                {
                    var workingArea = screen.WorkingArea;
                    IntPtr hConsole = GetConsoleWindow();
                    MoveWindow(hConsole, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, true);
                }
            }
            catch (Exception e) // Could be redirected
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        public static void UnredirectConsole(out IntPtr stdOut, out IntPtr stdIn, out IntPtr stdErr)
        {
            SetStdHandle(StdHandle.Output, stdOut = GetConsoleStandardOutput());
            SetStdHandle(StdHandle.Input, stdIn = GetConsoleStandardInput());
            SetStdHandle(StdHandle.Error, stdErr = GetConsoleStandardError());
        }

        private static IntPtr GetConsoleStandardInput()
        {
            var handle = CreateFile
                ("CONIN$"
                , DesiredAccess.GenericRead | DesiredAccess.GenericWrite
                , FileShare.ReadWrite
                , IntPtr.Zero
                , FileMode.Open
                , FileAttributes.Normal
                , IntPtr.Zero
                );
            if (handle == InvalidHandleValue)
                return InvalidHandleValue;
            return handle;
        }

        private static IntPtr GetConsoleStandardOutput()
        {
            var handle = CreateFile
                ("CONOUT$"
                , DesiredAccess.GenericWrite | DesiredAccess.GenericWrite
                , FileShare.ReadWrite
                , IntPtr.Zero
                , FileMode.Open
                , FileAttributes.Normal
                , IntPtr.Zero
                );
            if (handle == InvalidHandleValue)
                return InvalidHandleValue;
            return handle;
        }

        private static IntPtr GetConsoleStandardError()
        {
            var handle = CreateFile
                ("CONERR$"
                , DesiredAccess.GenericWrite | DesiredAccess.GenericWrite
                , FileShare.ReadWrite
                , IntPtr.Zero
                , FileMode.Open
                , FileAttributes.Normal
                , IntPtr.Zero
                );
            if (handle == InvalidHandleValue)
                return InvalidHandleValue;
            return handle;
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetStdHandle(StdHandle nStdHandle, IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile
            (string lpFileName
            , [MarshalAs(UnmanagedType.U4)] DesiredAccess dwDesiredAccess
            , [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode
            , IntPtr lpSecurityAttributes
            , [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition
            , [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes
            , IntPtr hTemplateFile
            );

        [Flags]
        private enum DesiredAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000
        }

        private enum StdHandle : int
        {
            Input = -10,
            Output = -11,
            Error = -12
        }

        private static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
    }
}