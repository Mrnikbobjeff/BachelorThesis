using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using BPTest.Shared.Repositories;
using Empathica.Shared;
using EmpaticaBindingLibrary;
using Realms;

namespace Empatica.iOS
{
    internal class EmpaticaiOSConnectionHandler : IEmpaticaConnectionHandler
    {
        private Device device;
        readonly EmpaticaiOSDelegate @delegate;
        public EmpaticaiOSConnectionHandler(EmpaticaSharedDevice sharedDevice, Device device)
        {
            this.device = device;
            @delegate = new EmpaticaiOSDelegate(sharedDevice);
        }

        public void Connect(IEmpaticaDevice device)
        {
            var actualDevice = device as ScannediOSEmpaticaDevice;
            actualDevice.Manager.ConnectWithDeviceDelegate(new EmpaticaiOSDeviceDelegate((x) => AddSensorData(x)));
        }

        void AddSensorData(SensorData s)
        {
            s.DeviceId = device.Id;
            using (var realm = Realm.GetInstance(SensorhubRealmConfiguration.Configuration))
            {
                realm.Write(() => realm.Add(s));
            }
        }

        public void Disconnect()
        {
            EmpaticaAPI.CancelDiscovery();
        }

        public void StartScanning(EmpaticaSharedDevice sharedDevice)
        {
            EmpaticaAPI.DiscoverDevicesWithDelegate(@delegate);
        }

        public void StopScanning()
        {
            EmpaticaAPI.CancelDiscovery();
        }
    }
}