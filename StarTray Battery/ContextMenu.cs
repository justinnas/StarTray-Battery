using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace StarTray_Battery
{
    public partial class StarTray_Battery : Form
    {
        private void InitializeContextMenu()
        {
            // Create the context menu for the icon
            contextMenu = new ContextMenu();

            contextMenu.MenuItems.Add(new MenuItem("StarTray Battery") { Enabled = false });
            contextMenu.MenuItems.Add("-");

            MenuItem colorModes = new MenuItem("Change Theme");

            MenuItem darkMode = new MenuItem("Dark Theme");
            darkMode.Click += DarkMode_Click;
            colorModes.MenuItems.Add(darkMode);

            MenuItem lightMode = new MenuItem("Light Theme");
            lightMode.Click += LightMode_Click;
            colorModes.MenuItems.Add(lightMode);

            MenuItem blueMode = new MenuItem("Blue11 Theme");
            blueMode.Click += Blue11Mode_Click;
            colorModes.MenuItems.Add(blueMode);

            contextMenu.MenuItems.Add(colorModes);

            MenuItem settings = new MenuItem("Options");
            startupMenuItem = new MenuItem("Run on Startup");
            startupMenuItem.Checked = IsTaskScheduled();
            startupMenuItem.Click += RunOnStartup_Click;
            settings.MenuItems.Add(startupMenuItem);
            settings.MenuItems.Add("-");
            MenuItem generateReport = new MenuItem("Generate battery report");
            generateReport.Click += GenerateReport_Click;
            settings.MenuItems.Add(generateReport);
            settings.MenuItems.Add(new MenuItem("v1.2 © justinnas") { Enabled = false });

            contextMenu.MenuItems.Add(settings);

            contextMenu.MenuItems.Add("-");

            MenuItem exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;
            contextMenu.MenuItems.Add(exitMenuItem);
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
            Close();
        }

        private void BatteryIconOnClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left) Process.Start("ms-settings:batterysaver");
        }


        private void GenerateReport_Click(object sender, EventArgs e)
        {
            GenerateBatteryReport();
        }

        private void GenerateBatteryReport()
        {
            string fileName = "battery_report_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".html";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullFilePath = path + "\\" + fileName;

            new ToastContentBuilder()
            .AddText("Generating battery report...")
            .AddText($"Report will be saved to your desktop as {fileName}")
            .SetToastDuration(ToastDuration.Short)
            .Show();

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C powercfg /batteryreport /output \"{fullFilePath}\" && start \"\" \"{fullFilePath}\"";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
