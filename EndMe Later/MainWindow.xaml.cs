using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EndMe_Later
{
    public partial class MainWindow
    {
        Timer t;
        public bool sleepEnabled, brightEnabled, volumeEnabled;

        public MainWindow()
        {
            InitializeComponent();
            t = new Timer();
            t.main = this;
        }

        // calculate sleep time in seconds and send to sdtimer
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (slider.Value != 0)
            {
                disableInputs();
                featureCheck();
                t.startTimer();
            }
            else
            {
                timeRemaining.Text = "Choose a time";
            }
        }

        // button for canceling shutdown
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            enableInputs();
        }

        private void featureCheck()
        {
            if (brightnessCheckBox.IsChecked == true)
            {
                brightEnabled = true;
            }
            if (sleepCheckBox.IsChecked == true)
            {
                sleepEnabled = true;
            }
            if (volumeCheckBox.IsChecked == true)
            {
                volumeEnabled = true;
            }
        }

        private void disableInputs()
        {
            slider.IsEnabled = false;
            sleepCheckBox.IsEnabled = false;
            volumeCheckBox.IsEnabled = false;
            brightnessCheckBox.IsEnabled = false;
            startButton.IsEnabled = false;
        }

        private void enableInputs()
        {
            slider.IsEnabled = true;
            sleepCheckBox.IsEnabled = true;
            volumeCheckBox.IsEnabled = true;
            brightnessCheckBox.IsEnabled = true;
            startButton.IsEnabled = true;
        }
    }
}
