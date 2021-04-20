using BPTest.Shared.Models;
using BPTest.Shared.Repositories;
using Realms;
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

        protected void InvokeConnectionStateChanged() => ConnectionStatusChanged?.Invoke(this, null);

        public DeviceStatus Status { get; protected set; }
        public abstract DeviceType DeviceType { get; }
        public abstract string Name { get; }
        public abstract ImageSource Icon { get; }


        readonly List<SensorData> buffer = new List<SensorData>(1000);
        readonly object synclock = new object();

        public void AddSensorData(SensorData s)
        {
            s.DeviceId = device.Id;
            OnSensorDataReceived(s);
            if (buffer.Count > 1000)
            {
                lock (synclock)
                {
                    if (buffer.Count > 1000)
                    {
                        using (var realm = Realm.GetInstance(SensorhubRealmConfiguration.Configuration))
                        {
                            realm.Write(() => realm.Add(buffer));
                        }
                        buffer.Clear();
                    }
                }
            }
            buffer.Add(s);
        }

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
        protected void OnSensorDataReceived(SensorData s) => SensorDataReceived?.Invoke(this, new SensorDataEventArgs { Value = s });
        public event EventHandler<SensorDataEventArgs> SensorDataReceived;
        public event EventHandler<EventArgs> ConnectionStatusChanged;
    }
}
