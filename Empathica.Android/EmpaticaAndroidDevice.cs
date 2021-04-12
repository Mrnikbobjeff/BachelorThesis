using System;
using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using Com.Empatica.Empalink;
using Empathica.Shared;

namespace Empathica.Android
{
    public class EmpaticaAndroidDevice : EmpaticaSharedDevice
    {
        public EmpaticaAndroidDevice(Device device) : base(device)
        {
            connectionHandler = new EmpaticaAndroidStatusDelegate(this);
        }

        internal void OnDeviceFound(EmpaticaDevice device)
        {
            throw new NotImplementedException();
        }
    }
    public class EmpaticaAndroidDeviceBuilder : EmpaticaDeviceBuilder
    {
        public override PhysicalDevice FromDevice(Device device)
        {
            return new EmpaticaAndroidDevice(device);
        }
    }
}