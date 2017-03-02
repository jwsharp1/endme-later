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

            if (!brightnessAvailability())  // disable the brightness feature if it is unsupported
            {
                brightnessDesc.Text = "Feature unavailable on this machine.";
                brightnessCheckBox.IsEnabled = false;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
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

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            t.stopTimer(false);
            updateTimeRemaining();
            enableInputs();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void ColorZone_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            App.Current.MainWindow.DragMove();
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

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updateTimeRemaining();
        }

        private void updateTimeRemaining()
        {
            string fmt = "00.##";
            int hr, min, sec, secRemain = (int)slider.Value;
            hr = secRemain / 3600;
            min = (secRemain % 3600) / 60;
            sec = secRemain % 60;
            string remTime = hr.ToString(fmt) + "h " + min.ToString(fmt) + "m " + sec.ToString(fmt) + "s";

            timeRemaining.Text = remTime;
        }
    }
}
