using System;
using System.Windows.Forms;
using System.Management;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using Microsoft.Win32.TaskScheduler;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace StarTray_Battery
{
    public partial class StarTray_Battery : Form
    {
        private static NotifyIcon notifyIcon;
        private static ContextMenu contextMenu;
        private int currentBatteryPercentage = 0;
        private string resourcesFolder = Path.Combine(Application.StartupPath, "Resources");

        private string colorMode = "light";
        private Color batteryColor = Color.White;
        private string batteryIconPath;
        private string batteryChargeIconPath;
        private string currentBatteryIconPath;
        private Image batteryIcon = null;

        private bool isCharging = false;

        private MenuItem startupMenuItem;

        private TaskService taskService = new TaskService();
        private const string TaskName = "StarTrayBattery_RunOnStartup";

        FontFamily customFontFamily = FontFamily.GenericSansSerif;

        public StarTray_Battery()
        {
            InitializeComponent();
            InitializeBatteryIcon();
        }

        private void InitializeBatteryIcon()
        {
            LoadSettings();

            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(Path.Combine(resourcesFolder, "font.ttf"));
            customFontFamily = fontCollection.Families[0];

            isCharging = SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;
            SystemEvents.PowerModeChanged += HandlePowerModeChanged;
            StartManagementEventWatcher();

            // Initialize NotifyIcon
            currentBatteryPercentage = GetBatteryPercentage();
            notifyIcon = new NotifyIcon();
            notifyIcon.MouseClick += BatteryIconOnClick;
            RefreshText();
            SwitchIconForCharge();
            notifyIcon.Icon = IconUtils.GenerateBatteryIconImage(currentBatteryPercentage, batteryIcon, batteryColor, customFontFamily, isCharging);

            InitializeContextMenu();
            notifyIcon.ContextMenu = contextMenu;

            notifyIcon.Visible = true;


            StartTimer();

            Application.Run();
        }

        private void StartTimer()
        {
            // Initialize Timer
            Timer timer = new Timer();
            timer.Interval = 30 * 1000; // Update every 30 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int newBatteryPercentage = GetBatteryPercentage();

            if (newBatteryPercentage != currentBatteryPercentage)
            {
                currentBatteryPercentage = newBatteryPercentage;    
                RefreshIcon();
            }

            // It also changes the estimated time remaining, it's better to always update because estimated time can change not depending on percentage
            RefreshText();

            GC.Collect();
        }

        private void RefreshIcon()
        {
            NativeMethods.DestroyIcon(notifyIcon.Icon.Handle);
            notifyIcon.Icon?.Dispose();
            batteryIcon.Dispose();
            SwitchIconForCharge();
            notifyIcon.Icon = IconUtils.GenerateBatteryIconImage(currentBatteryPercentage, batteryIcon, batteryColor, customFontFamily, isCharging);
            GC.Collect();
        }

        private void RefreshText()
        {
            notifyIcon.Text = "Battery status: " + currentBatteryPercentage.ToString() + "%";
            if (isCharging) {
                notifyIcon.Text += "\nCharging";
                return;
            } 

            TimeSpan batteryTime = TimeSpan.FromSeconds(SystemInformation.PowerStatus.BatteryLifeRemaining);

            string batteryTimeRemaining = "";

            if (batteryTime.Hours > 0 && batteryTime.Minutes > 0)
                batteryTimeRemaining = string.Format("{0}h {1}min", batteryTime.Hours, batteryTime.Minutes);
            else if (batteryTime.Minutes <= 0)
                batteryTimeRemaining = string.Format("{0}h", batteryTime.Hours);
            else
                batteryTimeRemaining = string.Format("{0}min", batteryTime.Minutes);


            if (batteryTime.TotalSeconds > 0 && !isCharging) notifyIcon.Text += "\n" + batteryTimeRemaining + " remaining";
            else notifyIcon.Text += "\nCalculating remaining";
        }

        private void SwitchIconForCharge()
        {
            if (isCharging)
            {
                currentBatteryIconPath = batteryChargeIconPath;
            }
            else
            {
                currentBatteryIconPath = batteryIconPath;
            }

            batteryIcon = Image.FromFile(currentBatteryIconPath);
        }


        private void HandlePowerModeChanged(object sender, EventArgs e)
        {
            isCharging = (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online);
            int newBatteryPercentage = GetBatteryPercentage();
            currentBatteryPercentage = newBatteryPercentage;
            RefreshText();
            RefreshIcon();
        }

        private void StartManagementEventWatcher()
        {
            ManagementEventWatcher watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_PowerManagementEvent WHERE EventType = 7");
            watcher.EventArrived += new EventArrivedEventHandler((sender, e) =>
            {
                SystemEvents.PowerModeChanged -= HandlePowerModeChanged;
                SystemEvents.PowerModeChanged += HandlePowerModeChanged;
                HandlePowerModeChanged(sender, e);
            });
            watcher.Query = query;
            watcher.Start();
        }

        private int GetBatteryPercentage()
        {
            // Retrieve battery percentage using Windows Management Instrumentation (WMI)
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT EstimatedChargeRemaining FROM Win32_Battery");
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject obj in collection)
            {
                if (obj.Properties["EstimatedChargeRemaining"].Value != null)
                {
                    int batteryPercentage = Convert.ToInt32(obj.Properties["EstimatedChargeRemaining"].Value);
                    return batteryPercentage;
                }
            }

            return 0; // Error: Unable to retrieve battery percentage
        }

        private static class NativeMethods // Used for clearing up GDI's and User's icon handles
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool DestroyIcon(IntPtr handle);
        }
    }
}
