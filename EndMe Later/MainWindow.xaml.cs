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
        dndTimer dndt = new dndTimer();
        int sleepCount = 0;
        bool sleeping;

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
                sleeping = true;
            }

            if (dndCheckBox.IsChecked == true)
            {
            }
        }

        // button for canceling shutdown
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (sleeping)
            {
                st.stopSleepTimer();
                sleeping = false;
            }
        }
    }
}
