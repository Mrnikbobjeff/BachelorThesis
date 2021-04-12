using BPTest.Shared.Abstractions;
using BPTest.Shared.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BPTest.Shared.Devices
{
    public interface IDeviceBuilder
    {
        string DeviceTypeName { get;}
        ImageSource DeviceTypeIcon { get; }
        bool IsBluetooth { get; }
        Dictionary<SensorDataType, bool> DefaultSelectedSensors { get; }
        bool IsDeviceSupported(ScanResult scanResult, BluetoothDevice bluetoothDevice);
        PhysicalDevice FromDevice(Device device);
    }
}
