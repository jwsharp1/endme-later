using System;
//using System.Windows;
using System.Windows.Forms;

namespace EndMe_Later
{
    public static class Sleep
    {
        public static void sleep()
        {
            Application.SetSuspendState(PowerState.Suspend, false, false);

            //----------------
            // Keep the following code in case a Shutdown option is added later
            //----------------

            /*System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "-s";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();*/
        }
    }
}
