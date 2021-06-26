using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Uixe.Watcher.Extensions
{
    public static class AppExtension
    {
        /// <summary>
        /// 设置窗口的显示状态
        /// Win32 函数定义为：http://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指示窗口如何被显示</param>
        /// <returns>如果窗体之前是可见，返回值为非零；如果窗体之前被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        /// <summary>
        /// 创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改变各种可视的记号。
        /// 系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // 指示窗口为普通显示
        private const int WS_SHOWNORMAL = 1;

        private const int SW_SHOW = 5;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOWNA = 8;

        /// <summary>
        /// 获取正在运行的程序，没有运行的程序则返回null
        /// </summary>
        /// <returns></returns>
        private static Process RunningInstance()
        {
            // 获取当前活动的进程
            Process currentProcess = Process.GetCurrentProcess();

            // 根据当前进程的进程名获得进程集合
            // 如果该程序运行，进程的数量大于1
            Process[] processcollection = Process.GetProcessesByName(currentProcess.ProcessName.Replace(".vshost", ""));
            foreach (Process process in processcollection)
            {
                // 如果进程ID不等于当前运行进程的ID以及运行进程的文件路径等于当前进程的文件路径
                // 则说明同一个该程序已经运行了，此时将返回已经运行的进程
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == process.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 显示已运行的程序
        /// </summary>
        /// <param name="instance"></param>
        private static void HandleRunningInstance(Process instance)
        {
            // 显示窗口
            ShowWindow(instance.MainWindowHandle, WS_SHOWNORMAL);

            // 把窗体放在前端
            SetForegroundWindow(instance.MainWindowHandle);
        }

        public static void RunOnlyOneInstance(Action _main)
        {
            using (Mutex mutex = new Mutex(true, Application.ProductName, out bool createNew))
            {
                if (createNew)
                {
                    _main?.Invoke();
                }
                // 程序已经运行的情况，则弹出消息提示并终止此次运行
                else
                {
                    var ins = RunningInstance();
                    if (ins != null)
                    {
                        HandleRunningInstance(ins);
                    }
                    else
                    {
                        MessageBox.Show("应用程序已经在运行中...");
                        System.Threading.Thread.Sleep(1000);
                    }
                    // 终止此进程并为基础操作系统提供指定的退出代码。
                    System.Environment.Exit(1);
                }
            }
        }
    }
}