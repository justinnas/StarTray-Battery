using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace StarTray_Battery
{
    public partial class StarTray_Battery : Form
    {
        private void LightMode_Click(object sender, EventArgs e)
        {
            ApplyTheme("light");
        }

        private void DarkMode_Click(object sender, EventArgs e)
        {
            ApplyTheme("dark");
        }

        private void Blue11Mode_Click(object sender, EventArgs e)
        {
            ApplyTheme("blue11");
        }

        private void ApplyTheme(string theme)
        {
            if (colorMode != theme)
            {
                colorMode = theme;

                switch (theme)
                {
                    case "light":
                        batteryColor = Color.White;
                        batteryIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_icon_light_32.png");
                        batteryChargeIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_charge_icon_light_32.png");
                        break;
                    case "dark":
                        batteryColor = Color.Black;
                        batteryIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_icon_dark_32.png");
                        batteryChargeIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_charge_icon_dark_32.png");
                        break;
                    case "blue11":
                        batteryColor = Color.FromArgb(151, 234, 255);
                        batteryIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_icon_blue11_32.png");
                        batteryChargeIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_charge_icon_blue11_32.png");
                        break;
                }

                RefreshIcon();
                SaveSettings();

                GC.Collect();
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.ColorMode = colorMode;
            Properties.Settings.Default.BatteryColor = batteryColor;
            Properties.Settings.Default.BatteryIconPath = batteryIconPath;
            Properties.Settings.Default.BatteryChargeIconPath = batteryChargeIconPath;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            colorMode = Properties.Settings.Default.ColorMode;
            batteryColor = Properties.Settings.Default.BatteryColor;

            batteryChargeIconPath = Properties.Settings.Default.BatteryChargeIconPath;
            batteryIconPath = Properties.Settings.Default.BatteryIconPath;

            // Check if it's the first startup
            if (batteryIconPath == string.Empty)
            {
                FirstStartupThemeSet();
            }
        }

        private void FirstStartupThemeSet()
        {
            // Detect if the user is using Windows 11 or newer
            if (Environment.OSVersion.Version.Build >= 22000)
            {
                colorMode = "blue11";
                batteryColor = Color.FromArgb(151, 234, 255);
                batteryIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_icon_blue11_32.png");
                batteryChargeIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_charge_icon_blue11_32.png");
            }
            else
            {
                // If a user is not using Windows 11, it will apply the theme based on system theme
                if (IsWindowsDarkModeEnabled())
                {
                    colorMode = "light";
                    batteryColor = Color.White;
                    batteryIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_icon_light_32.png");
                    batteryChargeIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_charge_icon_light_32.png");
                }
                else
                {
                    colorMode = "dark";
                    batteryColor = Color.Black;
                    batteryIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_icon_dark_32.png");
                    batteryChargeIconPath = Path.Combine(Application.StartupPath, "Resources", "battery_charge_icon_dark_32.png");
                }
            }

            SaveSettings();
        }

        private bool IsWindowsDarkModeEnabled()
        {
            try
            {
                int mode = (int)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "SystemUsesLightTheme", -1);

                if (mode == 0)
                    return true;
                else
                    return false;
            }
            catch
            {
            }

            return false;
        }
    }
}
