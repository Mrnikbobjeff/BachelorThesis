using System;
using Com.Empatica.Empalink;
using Empathica.Shared;

namespace Empathica.Android
{
    public class DiscoveredAndroidEmpatica : IEmpaticaDevice
    {
        public DiscoveredAndroidEmpatica(EmpaticaDevice device)
        {
            ActualDevice = device;
        }
        public string Adress { get => ActualDevice.Device.Address; }
        public EmpaticaDevice ActualDevice { get; }
        public string Name { get => ActualDevice.AdvertisingName; }
    }
}