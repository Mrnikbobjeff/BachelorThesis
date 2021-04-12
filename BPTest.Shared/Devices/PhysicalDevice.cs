using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BPTest.Shared.Devices
{
    public abstract class PhysicalDevice
    {
        protected readonly Device device;

        protected PhysicalDevice(Device device)
        {
            this.device = device;
        }

        public DeviceStatus Status { get; }
        public abstract DeviceType DeviceType { get; }
        public abstract string Name { get; }
        public abstract ImageSource Icon { get; }
        public virtual void Reconnect()
        {
            if (Status == DeviceStatus.CONNECTED || Status == DeviceStatus.CONNECTING)
            {
                return;
            }
            Connect();
        }
        public abstract void Disconnect();
        public abstract void Connect();
    }
}
