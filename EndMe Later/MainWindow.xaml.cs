using System.Windows;
using System.ComponentModel;
using EndMe_Later.Properties;
using System.Windows.Forms;
using System.Linq;

namespace EndMe_Later
{
    public partial class MainWindow
    {
        Timer t;
        Brightness b;

        public MainWindow()
        {
            InitializeComponent();
            t = new Timer(this);

            if (!brightnessAvailability())
            {
                brightnessDesc.Text = "Feature unavailable on this machine.";
                brightnessCheckBox.IsEnabled = false;
            }
        }

        // calculate sleep time in seconds and send to sdtimer
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (slider.Value != 0)
            {
                disableInputs();
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

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr = System.Windows.Forms.MessageBox.Show("Are you sure you want to quit?\nAny running timers will be stopped.", "Quit?", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Settings.Default.Save();
            }
            else
            {
                e.Cancel = true;
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
            if(brightnessAvailability()) { brightnessCheckBox.IsEnabled = true; }
            startButton.IsEnabled = true;
        }

        private bool brightnessAvailability()
        {
            b = new Brightness();
            byte[] temp = b.GetBrightnessLevels();

            if (temp.Count() == 0)
            {
                return false;
            }
            else return true;
        }
    }
}
