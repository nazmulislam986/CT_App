using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_RESTORE = 9;

        [STAThread]
        private static void Main()
        {
            var proc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).FirstOrDefault(p => p.Id != Process.GetCurrentProcess().Id);
            if (proc == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CT_Mine());
            }
            else if (proc.MainWindowHandle == IntPtr.Zero && MessageBox.Show("App Running in Background, Exit?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (proc.MainWindowHandle != IntPtr.Zero)
            {
                ShowWindow(proc.MainWindowHandle, SW_RESTORE);
                SetForegroundWindow(proc.MainWindowHandle);
            }
        }
    }
}
