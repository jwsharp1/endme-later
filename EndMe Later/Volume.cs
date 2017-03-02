using NAudio.CoreAudioApi;
using System;
using System.Runtime.InteropServices;

namespace EndMe_Later
{
    class Volume
    {
        private int startVolume;
        private int tenthVolume;

        public void setStartVolume()
        {
            startVolume = GetVolume();
        }

        public int getStartVolume()
        {
            return startVolume;
        }

        public void setTenthVolume()
        {
            tenthVolume = (int)(Math.Ceiling(getStartVolume() / 10.0));
        }

        public int getTenthVolume()
        {
            return tenthVolume;
        }

        private int GetVolume()
        {
            int result = 100;
            try
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                MMDevice device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                result = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public void SetVolume(int value)
        {
            try
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                MMDevice device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)value / 100.0f;
            }
            catch (Exception)
            {
            }
        }
    }
}
