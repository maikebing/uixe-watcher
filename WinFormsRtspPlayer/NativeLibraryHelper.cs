using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRtspPlayer
{
    internal static class NativeLibraryHelper
    {
        #region dlopen

        private const int RTLD_LAZY = 0x00001; //Only resolve symbols as needed
        private const int RTLD_GLOBAL = 0x00100; //Make symbols available to libraries loaded later
        [DllImport("dl")]
        private static extern IntPtr dlopen(string file, int mode);

        #endregion

        #region LoadLibraryEx

        [System.Flags]
        private enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        #endregion

        static Dictionary<string, IntPtr> libs = new Dictionary<string, IntPtr>();
        internal static IntPtr LoadNixLibrary(string path)
        {
            return dlopen(path, RTLD_LAZY | RTLD_GLOBAL);
        }

        internal static IntPtr LoadWindowsLibrary(string path)
        {
            return LoadLibraryEx(path, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_SEARCH_APPLICATION_DIR | LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS | LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LoadLibraryFlags.LOAD_LIBRARY_SEARCH_SYSTEM32 | LoadLibraryFlags.LOAD_LIBRARY_SEARCH_USER_DIRS);
        }
        internal static void LoadNativeLibrary()
        {
            LoadNativeLibrary(AppContext.BaseDirectory);
        }


        internal static void LoadNativeLibrary(string targetDirectory)
        {
            var Platform = Environment.OSVersion.Platform;
            var files = System.IO.Directory.EnumerateFiles(System.IO.Path.Combine(targetDirectory, "lib", Environment.Is64BitProcess ? "x64" : "x86"), Platform == PlatformID.Win32NT ? "*.dll" : "*.so");

            foreach (var path in files)
            {
                IntPtr ptr = IntPtr.Zero;
                if (Platform == PlatformID.Win32NT)
                {
                    ptr = LoadWindowsLibrary(path);
                }
                else if (Platform == PlatformID.Unix || Platform == PlatformID.Other)
                {
                    ptr = LoadNixLibrary(path);
                }
                libs.Add(path, ptr);
            }
        }

    }
}
