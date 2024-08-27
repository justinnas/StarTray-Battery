using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace StarTray_Battery
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string currentProcessName = Process.GetCurrentProcess().ProcessName;

            var runningProcesses = Process.GetProcessesByName(currentProcessName);

            if (runningProcesses.Length > 1)
            {
                MessageBox.Show("StarTray Battery is already running!\n\nDon't see the icon?\nTry clicking the upward arrow in the system tray to see if it's hidden.", "StarTray Battery", MessageBoxButtons.OK);
                Environment.Exit(0);
                return;
            }

            // Prevent the exception from being displayed and simply exit the application without any notification
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            Application.ThreadException += Application_ThreadException;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new StarTray_Battery());
            }
            catch { }
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
