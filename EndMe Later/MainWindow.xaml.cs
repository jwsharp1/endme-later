using System.Windows;

namespace EndMe_Later
{
    public partial class MainWindow
    {
        public const int HOUR_IN_SECONDS = 3600;
        SleepTimer st = new SleepTimer();
        bool sleeping;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new SleepTimer();
            st.main = this;
        }

        // calculate sleep time in seconds and send to sdtimer
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (sleepCheckBox.IsChecked == true)
            {
                int sleepValue = (int)(slider.Value * HOUR_IN_SECONDS);
                st.makeSleepTimer(sleepValue);
                sleeping = true;
            }
        }

        // button for canceling shutdown
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (sleeping)
            {
                st.stopSleepTimer();
                timeRemaining.Text = "Stopped";
                sleeping = false;
            }
        }
    }
}
