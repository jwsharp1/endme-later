using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;

namespace EndMe_Later
{
    class dndTimer
    {
        private static string KEY_NOTIFICATION_SETTINGS = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings";
        private static string VALUE_NOTIFICATIONS_ENABLED = "NOC_GLOBAL_SETTING_TOASTS_ENABLED";
        RegistryKey dndKey;
        public bool dndStatus = (int)Registry.GetValue(KEY_NOTIFICATION_SETTINGS, VALUE_NOTIFICATIONS_ENABLED, 1) == 0;

        public bool getDndStatus()
        {
            if ((int)Registry.GetValue(KEY_NOTIFICATION_SETTINGS, VALUE_NOTIFICATIONS_ENABLED, 1) == 0)
            {
                dndStatus = true;
            }
            else
            {
                dndStatus = false;
            }
            return dndStatus;
        }

        public void turnOn()
        {
            dndKey = Registry.CurrentUser.OpenSubKey("SOFTWARE/Microsoft/Windows/CurrentVersion/Notifications/Settings", true);
            //Registry.SetValue(KEY_NOTIFICATION_SETTINGS, VALUE_NOTIFICATIONS_ENABLED, "1", RegistryValueKind.DWord);
            int i = dndKey.SubKeyCount;
            MessageBox.Show("There are " + i + " number of subkeys.");
            try
            {
                dndKey.SetValue("NOC_GLOBAL_SETTING_TOASTS_ENABLED", "1", RegistryValueKind.DWord);
                dndKey.Close();
            }
            catch
            {
                MessageBox.Show("There was an error.");
            }
        }

        public void turnOff()
        {
            dndKey = Registry.CurrentUser.OpenSubKey("SOFTWARE/Microsoft/Windows/CurrentVersion/Notifications/Settings", true);
            //Registry.SetValue(KEY_NOTIFICATION_SETTINGS, VALUE_NOTIFICATIONS_ENABLED, "0", RegistryValueKind.DWord);
            try
            {
                dndKey.SetValue("NOC_GLOBAL_SETTING_TOASTS_ENABLED", "0", RegistryValueKind.DWord);
                dndKey.Close();
            }
            catch
            {
                MessageBox.Show("There was an error.");
            }
        }
    }
}
