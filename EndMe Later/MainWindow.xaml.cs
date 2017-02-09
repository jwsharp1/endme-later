using System;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Forms;

namespace EndMe_Later
{
    public partial class MainWindow : System.Windows.Window
    {
        SleepTimer st = new SleepTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        // calculate sleep time in seconds and send to sdtimer
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (sleepCheckBox.IsChecked == true)
            {
                int sleepValue = (int)(sleepSlider.Value * 3600);
                st.makeSleepTimer(sleepValue);
            }
        }

        // button for canceling shutdown
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            st.stopSleepTimer();
        }
    }
}
