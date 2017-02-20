using System.Windows;

namespace EndMe_Later
{
    public partial class MainWindow
    {
        public const int HOUR_IN_SECONDS = 36;
        Timer t = new Timer();
        bool sleeping;

        public MainWindow()
        {
            InitializeComponent();
            t.main = this;
        }

        // calculate sleep time in seconds and send to sdtimer
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (slider.Value != 0)
            {
                int sliderValue = (int)(slider.Value * HOUR_IN_SECONDS);
                t.makeTimer(sliderValue);

                if (sleepCheckBox.IsChecked == true)
                {
                    sleeping = true;
                }
            }
        }

        // button for canceling shutdown
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            t.stopTimer();
            timeRemaining.Text = "Stopped";
            if (sleeping)
            {
                sleeping = false;
            }
        }
    }
}
