using System;
//using System.Windows;
using System.Windows.Forms;

namespace EndMe_Later
{
    public class Sleep
    {
        public void sleep()
        {
            Application.SetSuspendState(PowerState.Suspend, false, false);
            /*startInfo.FileName = "rundll32";
            startInfo.Arguments = "powrprof.dll,SetSuspendState 0,1,0";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
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
