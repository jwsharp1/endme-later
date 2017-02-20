using System;
using System.Windows;
using System.Windows.Threading;

namespace EndMe_Later
{
    public class Timer
    {
        int setTime = 0;
        DispatcherTimer progTimer;
        public MainWindow main;
        private Sleep sl = new Sleep();
        private Volume v = new Volume();
        bool timerStatus;
        int currentValue, threeQuarter, half, quarter;
        bool tQDone, hDone, qDone;

        private DateTime TimerStart { get; set; }

        // create the timer with the provided amount of time and start it
        public void makeTimer(int args)
        {
            this.TimerStart = DateTime.Now;
            progTimer = new DispatcherTimer();
            progTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            progTimer.Tick += Timer_Tick;
            progTimer.Start();
            timerStatus = true;

            setTime = args;
            // set the milestones for the volume reducer
            createMilestones();
        }

        private void createMilestones()
        {
            threeQuarter = (int)(setTime * .75);
            half = (int)(setTime * .5);
            quarter = (int)(setTime * .25);

            tQDone = false;
            hDone = false;
            qDone = false;
        }

        public void stopTimer()
        {
            if (timerStatus)
            {
                progTimer.Stop();
            }
        }

        // handles what happens when the timer ticks
        private void Timer_Tick(object sender, EventArgs e)
        {
            currentValue = (int)(DateTime.Now - this.TimerStart).TotalSeconds;

            if (setTime == currentValue)
            {
                progTimer.Stop();
                sl.sleep();
                //displayMessage();
            }
            
            if (main.volumeCheckBox.IsChecked == true)
            {
                if (currentValue == threeQuarter)
                {
                    if (!tQDone)
                    {
                        v.SetVolume((int)(v.GetVolume() * .75));
                        tQDone = true;
                    }
                }
                if (currentValue == half)
                {
                    if (!hDone)
                    {
                        v.SetVolume((int)(v.GetVolume() * .5));
                        hDone = true;
                    }
                }
                if (currentValue == quarter)
                {
                    if (!qDone)
                    {
                        v.SetVolume((int)(v.GetVolume() * .25));
                        qDone = true;
                    }
                }
            }
            setRemainingTime(currentValue);
        }

        public void setRemainingTime(int currTime)
        {
            int hr, min, sec, secRemain = (setTime - currTime);
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
