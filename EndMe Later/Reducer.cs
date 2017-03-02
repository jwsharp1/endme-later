using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndMe_Later
{
    class Reducer
    {
        private Volume v;
        private Brightness b;
        public MainWindow main;
        private bool first_V, first_B;
        private int lastVolume, tenthVolume, tenthBrightness, lastBrightness;

        public Reducer(MainWindow main)
        {
            this.main = main;
            if (main.volumeCheckBox.IsChecked == true)
            {
                v = new Volume();
                v.setStartVolume();
                v.setTenthVolume();
                tenthVolume = v.getTenthVolume();
                lastVolume = v.getStartVolume();
            }
            if (main.brightnessCheckBox.IsChecked == true)
            {
                b = new Brightness();
                b.setStartBrightness();
                b.setTenthBrightness();
                tenthBrightness = b.getTenthBrightness();
                lastBrightness = b.getStartBrightness();
            }
        }

        public void reduce()
        {
            if(main.volumeCheckBox.IsChecked == true)
            {
                reduceVolume();
            }

            if (main.brightnessCheckBox.IsChecked == true)
            {
                reduceBrightness();
            }
        }

        private void reduceVolume()
        {
            if (first_V)
            {
                v.SetVolume(lastVolume - tenthVolume);
                int test = lastVolume - v.getTenthVolume();
                lastVolume = lastVolume - tenthVolume;
            }
            first_V = true;
        }

        private void reduceBrightness()
        {
            if (first_B)
            {
                b.SetBrightness((byte)(lastBrightness - tenthBrightness));
                lastBrightness = lastBrightness - tenthBrightness;
            }
            first_B = true;
        }
    }
}
