using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;

namespace StarTray_Battery
{
    public partial class StarTray_Battery : Form
    {
        private void RunOnStartup_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
            if (!IsTaskScheduled())
            {
                CreateTask();
            }
            else
            {
                RemoveTask();
            }
        }

        private bool IsTaskScheduled()
        {
            Task task = taskService.GetTask(TaskName);
            return task != null;
        }

        private void CreateTask()
        {
            TaskDefinition taskDefinition = taskService.NewTask();
            taskDefinition.RegistrationInfo.Description = "Start StarTray Battery on system startup.";
            taskDefinition.Triggers.Add(new LogonTrigger());
            taskDefinition.Actions.Add(new ExecAction(Application.ExecutablePath));
            taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;
            taskDefinition.Settings.DisallowStartIfOnBatteries = false;
            taskDefinition.Settings.StopIfGoingOnBatteries = false;
            taskDefinition.Settings.RunOnlyIfIdle = false;
            taskDefinition.Settings.IdleSettings.StopOnIdleEnd = false;
            taskDefinition.Settings.RunOnlyIfNetworkAvailable = false;
            taskDefinition.Settings.ExecutionTimeLimit = TimeSpan.Zero;
            taskDefinition.Settings.StartWhenAvailable = true;

            taskService.RootFolder.RegisterTaskDefinition(TaskName, taskDefinition);
        }

        private void RemoveTask()
        {
            if (taskService == null)
            {
                taskService = new TaskService();
            }

            taskService.RootFolder.DeleteTask(TaskName, false);
        }
    }
}
