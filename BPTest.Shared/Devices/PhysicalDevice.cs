using BPTest.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public virtual Task Reconnect()
        {
            if (Status == DeviceStatus.CONNECTED || Status == DeviceStatus.CONNECTING)
            {
                return Task.CompletedTask;
            }
            return Connect();
        }
        public abstract Task Disconnect();
        public abstract Task Connect();
        protected void OnSensorDataReceived(SensorData s) => SensorDataReceived.Invoke(this, new SensorDataEventArgs { Value = s });
        public event EventHandler<SensorDataEventArgs> SensorDataReceived;
    }
}
