﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EndMe_Later
{
    public partial class MainWindow
    {
        Timer t = new Timer();

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
                slider.IsEnabled = false;
                sleepCheckBox.IsEnabled = false;
                volumeCheckBox.IsEnabled = false;
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
            t.stopTimer();
            slider.IsEnabled = true;
            sleepCheckBox.IsEnabled = true;
            volumeCheckBox.IsEnabled = true;
        }
    }
}
