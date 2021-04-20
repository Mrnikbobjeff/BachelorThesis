using BPTest.Shared.Abstractions;
using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using BPTest.Shared.Repositories;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Empathica.Shared
{
    public class EmpaticaSharedDevice : ScannableDevice
    {
        protected IEmpaticaConnectionHandler connectionHandler;
        IEmpaticaDevice empatica;

        public static string ApiKey { get; } = "05a6751e667540f684cc592f056acdda";
        public EmpaticaSharedDevice(BPTest.Shared.Devices.Device device) : base(device)
        {
        }

        public override DeviceType DeviceType => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override ImageSource Icon => throw new NotImplementedException();


        public override async Task Connect()
        {
            await Task.Delay(5000);
            connectionHandler.StartScanning(this);
        }

        public override Task Disconnect()
        {
            connectionHandler.Disconnect();
            return Task.CompletedTask;
        }

        public void OnDeviceFound(IEmpaticaDevice empaticaDevice)
        {
            if(device.PairedDeviceAdress == null)
            {
                device.PairedDeviceAdress = empaticaDevice.Adress;
                using (var realm = Realm.GetInstance(SensorhubRealmConfiguration.Configuration))
                {
                    var trackedDevice = realm.All<BPTest.Shared.Devices.Device>().First(x => x.Id == device.Id);
                    realm.Write(() => trackedDevice.PairedDeviceAdress = empaticaDevice.Adress);
                }
            }

            if (device.PairedDeviceAdress == empaticaDevice.Adress)
            {
                empatica = empaticaDevice;
                connectionHandler.StopScanning();
                connectionHandler.Connect(empaticaDevice);
            }
        }
    }

    public abstract class EmpaticaDeviceBuilder : IDeviceBuilder
    {
        public string DeviceTypeName => "Empatica E4";
        public ImageSource DeviceTypeIcon => ImageSource.FromFile("");
        public bool IsBluetooth => true;
        public Dictionary<SensorDataType, bool> DefaultSelectedSensors => new Dictionary<SensorDataType, bool>
        {
            { SensorDataType.ACCELERATION_X, true },
            { SensorDataType.ACCELERATION_Y, true },
            { SensorDataType.ACCELERATION_Z, true },
            { SensorDataType.TAG, true },
            { SensorDataType.INTER_BEAT_INTERVAL, true },
            { SensorDataType.GALVANIC_SKIN_RESPONSE, true },
            { SensorDataType.BLOOD_VOLUME_PULSE, true },
            { SensorDataType.TEMPERATURE_LOCAL, true }
        };

        public abstract PhysicalDevice FromDevice(BPTest.Shared.Devices.Device device);

        public bool IsDeviceSupported(ScanResult scanResult, BluetoothDevice bluetoothDevice)
        {
            return scanResult.DeviceName?.Contains("Empatica") ?? false;
        }
    }
}
