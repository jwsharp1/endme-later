using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace EndMe_Later
{
    public class Timer
    {
        DispatcherTimer progTimer;
        public MainWindow main;
        private Sleep sl = new Sleep();
        private Volume v = new Volume();
        public const int HOUR_IN_SECONDS = 3600;
        private int setTime, currentValue, threeQuarter, half, quarter;
        private bool timerStatus, tQDone, hDone, qDone;

        private DateTime TimerStart { get; set; }

        // create the timer with the provided amount of time and start it
        public void startTimer()
        {
            if (timerStatus == false)
            {
                this.TimerStart = DateTime.Now;
                progTimer = new DispatcherTimer();
                progTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                progTimer.Tick += Timer_Tick;   // Timer_Tick fires upon every tick
                progTimer.Start();

                timerStatus = true;
                setTime = (int)(main.slider.Value * HOUR_IN_SECONDS);

                if(main.volumeCheckBox.IsChecked == true)
                {
                    createVolMilestones();     // set the milestones for the volume reducer
                }

                PreventSleepAndMonitorOff();    // disable autosleep and keep the monitor from turning itself off
            }
        }

        private void createVolMilestones()
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
                timerStatus = false;
                AllowSleep();
            }
        }

        // handles what happens when the timer ticks
        private void Timer_Tick(object sender, EventArgs e)
        {
            currentValue = (int)(DateTime.Now - this.TimerStart).TotalSeconds;

            if (setTime == currentValue)    // when the timer runs out
            {
                progTimer.Stop();
                if(main.sleepCheckBox.IsChecked == true)
                {
                    AllowSleep();   // allow computer to sleep
                    sl.sleep();
                }
            }

            if (main.volumeCheckBox.IsChecked == true)
            {
                checkReduceVolume();
            }
            setDisplayedTime(currentValue);
        }

        public void checkReduceVolume()
        {
            if (currentValue == threeQuarter)
            {
                if (!tQDone)
                {
                    v.SetVolume((int)(v.GetVolume() * .25));
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
                    v.SetVolume((int)(v.GetVolume() * .75));
                    qDone = true;
                }
            }
        }

        public void setDisplayedTime(int currTime)
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

        private void PreventSleepAndMonitorOff()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED | NativeMethods.ES_DISPLAY_REQUIRED);
        }

        private void AllowSleep()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS);
        }
    }

    internal static class NativeMethods
    {
        // Import SetThreadExecutionState Win32 API and necessary flags
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
    }
}
