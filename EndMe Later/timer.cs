using System;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace EndMe_Later
{
    public class Timer
    {
        DispatcherTimer progTimer;
        private DateTime TimerStart { get; set; }
        private MainWindow main;
        private Reducer r;
        protected int timerSetTime_Sec, currentTimerValue_Sec, tenthTime;
        private bool timerOn, once = false;

        public Timer(MainWindow main)
        {
            this.main = main;
        }

        public void startTimer()
        {
            if (timerOn == false)
            {
                createTimer();
                progTimer.Start();
                timerOn = true;
                timerSetTime_Sec = (int)main.slider.Value;  // get the amt of time from the slider
                tenthTime = (int)(timerSetTime_Sec / 10);   // calculate 1/10 of the timer's time for the reducer milestones

                if (main.volumeCheckBox.IsChecked == true || main.brightnessCheckBox.IsChecked == true)     // if at least one reducer is active
                {
                    r = new Reducer(main);      // create the reducer object
                }

                if (main.dndCheckBox.IsChecked == true)
                {
                    dndTimer.turnOn();     // turn on dnd
                }

                KeepPCAwake();    // disable autosleep and keep the monitor from turning itself off
            }
        }

        private void createTimer()
        {
            this.TimerStart = DateTime.Now;
            progTimer = new DispatcherTimer();
            progTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            progTimer.Tick += Timer_Tick;   // Timer_Tick fires upon every tick
        }

        public void stopTimer(bool b)
        {
            if (timerOn)
            {
                progTimer.Stop();
                timerOn = false;
                AllowSleep();
                if (main.dndCheckBox.IsChecked == true) { dndTimer.turnOff(); }   // turn of dnd
                if (b == true) { checkSleep(); }    // if stopTimer was called by the timer expiring w/ sleep feature enabled
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTimerValue_Sec = (int)(DateTime.Now - this.TimerStart).TotalSeconds;     // calculate the time that has passed

            if(main.brightnessCheckBox.IsChecked == true || main.volumeCheckBox.IsChecked == true)    // if one of the reducer options is active
            {
                reduceHandler();
            }

            if(currentTimerValue_Sec == timerSetTime_Sec)   // when the timer expires
            {
                stopTimer(true);
            }

            setDisplayedTime(currentTimerValue_Sec);    // update the displayed time
        }

        private void setDisplayedTime(int currTime)
        {
            string fmt = "00.##";
            int hr, min, sec, secRemain = (timerSetTime_Sec - currTime);
            hr = secRemain / 3600;
            min = (secRemain % 3600) / 60;
            sec = secRemain % 60;
            string remTime = hr.ToString(fmt) + "h " + min.ToString(fmt) + "m " + sec.ToString(fmt) + "s";

            main.timeRemaining.Text = remTime;
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

        private void checkSleep()
        {
            if (main.sleepCheckBox.IsChecked == true)
            {
                Sleep.sleep();
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
