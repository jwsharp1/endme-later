using System;
using System.Windows;
using System.Windows.Threading;

namespace EndMe_Later
{
    public class SleepTimer
    {
        int sleepTime = 0;
        DispatcherTimer sleepTimer;
        public MainWindow main;
        int currentValue;

        private DateTime TimerStart { get; set; }

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
            currentValue = (DateTime.Now - this.TimerStart).Seconds;

            if (sleepTime == currentValue)
            {
                sleepTimer.Stop();
                //sleep();
                displayMessage();
            }
            setRemainingTime(currentValue);
        }

        public void setRemainingTime(int currTime)
        {
            int hr, min, sec, secRemain = (sleepTime - currTime);
            hr = secRemain / 3600;
            min = (secRemain % 3600) / 60;
            sec = secRemain % 60;
            string remTime = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();

            main.timeRemaining.Text = remTime;
        }

        // debug method, use this instead of sleep() to test timer functionality
        private void displayMessage()
        {
            MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.OK);
        }
    }
}
