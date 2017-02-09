using System;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndMe_Later
{
    class SleepTimer
    {
        DispatcherTimer sleepTimer, dndTimer;

        private void sleep()
        {
            /*System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "rundll32";
            startInfo.Arguments = "powrprof.dll,SetSuspendState 0,1,0";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();*/
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "-s";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
        }

        // create the timer with the provided amount of time and start it
        public void makeSleepTimer(int args)
        {
            sleepTimer = new DispatcherTimer();
            sleepTimer.Interval = new TimeSpan(0, 0, args);
            sleepTimer.Tick += sleepTimer_Tick;
            sleepTimer.Start();
        }

        public void stopSleepTimer()
        {
            sleepTimer.Stop();
        }

        // handles what happens when the timer ticks
        private void sleepTimer_Tick(object sender, EventArgs e)
        {
            sleepTimer.Stop();
            sleep();
        }
    }
}
