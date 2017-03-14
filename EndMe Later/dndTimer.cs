using System;
using Microsoft.Win32;
using System.Diagnostics;

namespace EndMe_Later
{
    class dndTimer
    {
        private static string KEY_NOTIFICATION_SETTINGS = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings";
        private static string VALUE_NOTIFICATIONS_ENABLED = "NOC_GLOBAL_SETTING_TOASTS_ENABLED";
        private static string EXPLORER = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe");
        private RegistryKey dndKey;
        private ProcessStartInfo killExplorer, startExplorer;
        private Process process;
        private bool dndStatus;

        public bool getDndStatus()
        {
            dndStatus = (int)Registry.GetValue(KEY_NOTIFICATION_SETTINGS, VALUE_NOTIFICATIONS_ENABLED, 1) == 0;
            if (dndStatus)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void turnOn()
        {
            dndKey = Registry.CurrentUser.OpenSubKey(KEY_NOTIFICATION_SETTINGS, true);
            if(dndKey != null)
            {
                dndKey.SetValue(VALUE_NOTIFICATIONS_ENABLED, 0, RegistryValueKind.DWord);
                dndKey.Close();
            }
            restartExplorer();
        }

        public void turnOff()
        {
            dndKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings", true);
            if (dndKey != null)
            {
                dndKey.SetValue(VALUE_NOTIFICATIONS_ENABLED, 1, RegistryValueKind.DWord);
                dndKey.Close();
            }
            restartExplorer();
        }

        private void restartExplorer()
        {
            killExplorer = new ProcessStartInfo("taskkill", "/F /IM explorer.exe");
            process = new Process();
            process.StartInfo = killExplorer;
            killExplorer.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            process = new Process();
            process.StartInfo.FileName = EXPLORER;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }
    }
}
