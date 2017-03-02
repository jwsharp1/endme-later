using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace EndMe_Later
{
    public class Timer
    {
        DispatcherTimer progTimer;
        private DateTime TimerStart { get; set; }
        private MainWindow main;
        private Sleep sl;
        private Reducer r;
        private const int HOUR_IN_SECONDS = 100;
        protected int timerSetTime_Sec, currentTimerValue_Sec, tenthTime;
        private bool timerOn, once = false;

        public Timer(MainWindow main)
        {
            this.main = main;
        }

        // create the timer with the provided amount of time and start it
        public void startTimer()
        {
            if (timerOn == false)
            {
                createTimer();
                progTimer.Start();
                timerOn = true;
                timerSetTime_Sec = (int)(main.slider.Value * HOUR_IN_SECONDS);
                tenthTime = (int)(timerSetTime_Sec / 10);

                // if at least one reducer is active
                if (main.volumeCheckBox.IsChecked == true || main.brightnessCheckBox.IsChecked == true)
                {
                    r = new Reducer(main);      // create the reducer object
                }

                    KeepPCAwake();    // disable autosleep and keep the monitor from turning itself off
            }
        }

        public void stopTimer()
        {
            if (timerOn)
            {
                progTimer.Stop();
                timerOn = false;
                AllowSleep();
                checkSleep();
            }
        }

        // handles what happens when the timer ticks
        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTimerValue_Sec = (int)(DateTime.Now - this.TimerStart).TotalSeconds;

            if(main.brightnessCheckBox.IsChecked == true || main.volumeCheckBox.IsChecked == true)
            {
                reduceHandler();    // if one of the reducer options is active, execute this
            }

            if(currentTimerValue_Sec == timerSetTime_Sec)
            {
                stopTimer();
            }

            setDisplayedTime(currentTimerValue_Sec);    // update the displayed time
        }

        public void setDisplayedTime(int currTime)
        {
            int hr, min, sec, secRemain = (timerSetTime_Sec - currTime);
            hr = secRemain / 3600;
            min = (secRemain % 3600) / 60;
            sec = secRemain % 60;
            string remTime = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();

            main.timeRemaining.Text = remTime;
        }

        private void createTimer()
        {
            this.TimerStart = DateTime.Now;
            progTimer = new DispatcherTimer();
            progTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            progTimer.Tick += Timer_Tick;   // Timer_Tick fires upon every tick
        }

        private void reduceHandler()
        {
            if (currentTimerValue_Sec % tenthTime == 0 && once == false) // when a 10% milestone is reached
            {
                r.reduce();
                once = true;    // to avoid reducing multiple times in a second
            }

            if (currentTimerValue_Sec % tenthTime == 1)  // reset the "once" bool after the 10% milestone is over
            {
                once = false;
            }
        }

        // debug method, use this instead of sleep() to test timer functionality
        private void displayMessage()
        {
            MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.OK);
        }

        private void checkSleep()
        {
            if (main.sleepCheckBox.IsChecked == true)
            {
                sl = new Sleep();
                sl.sleep();
            }
        }

        private void KeepPCAwake()
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
