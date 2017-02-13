using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndMe_Later
{
    public class SleepTimer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int sleepTime = 0;

        DispatcherTimer sleepTimer, dndTimer;

        private void sleep()
        {
            /*startInfo.FileName = "rundll32";
            startInfo.Arguments = "powrprof.dll,SetSuspendState 0,1,0";*/
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "-s";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
        }

        private DateTime TimerStart { get; set; }

        // create the timer with the provided amount of time and start it
        public void makeSleepTimer(int args)
        {
            this.TimerStart = DateTime.Now;
            sleepTimer = new DispatcherTimer();
            sleepTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            sleepTimer.Tick += sleepTimer_Tick;
            sleepTime = args;
            sleepTimer.Start();
        }

        public void stopSleepTimer()
        {
            sleepTimer.Stop();
        }

        // handles what happens when the timer ticks
        private void sleepTimer_Tick(object sender, EventArgs e)
        {
            var currentValue = DateTime.Now - this.TimerStart;
            if (sleepTime == currentValue.Seconds)
            {
                sleepTimer.Stop();
                sleep();
            }
            //this.seconds.Text = currentValue.Seconds.ToString();
        }

        public string getRemainingTime()
        {
            return sleepTimer.Interval.TotalHours + ":" + sleepTimer.Interval.TotalMinutes;
        }
    }
}
