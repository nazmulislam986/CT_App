using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcesses().Count<Process>((Process p) => p.ProcessName == processName) <= 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CT_Mine());
            }
            else
            {
                MessageBox.Show("Already Opened, See Below The Task Bar");
            }
        }
    }
}
