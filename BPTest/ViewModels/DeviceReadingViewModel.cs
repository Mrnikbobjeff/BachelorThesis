using BPTest.Models;
using BPTest.Repositories;
using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using BPTest.Shared.Repositories;
using BPTest.Utils;
using Microcharts;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BPTest.ViewModels
{
    public class DeviceReadingViewModel : BaseViewModel
    {
        Realm realm = Realm.GetInstance(SensorhubRealmConfiguration.Configuration);
        private readonly IDeviceBuilderFactory deviceBuilderFactory;
        PhysicalDevice device;
        private ObservableCollection<SensorChart> charts = new ObservableCollection<SensorChart>();

        public ObservableCollection<SensorChart> Charts { get => charts; set => SetProperty(ref charts, value); }

        public DeviceReadingViewModel(IDeviceBuilderFactory deviceBuilderFactory)
        {
            this.deviceBuilderFactory = deviceBuilderFactory;
        }

        public void LoadDevice(int id)
        {
            var now = DateTimeOffset.Now;
            var allOfType = realm.All<SensorData>().Where(x => x.DeviceId == id && x.Time > now);
            DataTypes.Clear();
            var savedDevice = realm.All<Device>().Where(x => x.Id == id).Single().Clone();
            var deviceBuilder = deviceBuilderFactory.ForDeviceModule(savedDevice.Module);
            device = deviceBuilder.FromDevice(savedDevice);
            DataTypes.AddRange(deviceBuilder.DefaultSelectedSensors.Keys);
            device.Connect();

            device.SensorDataReceived += Device_SensorDataReceived;
        }

        private void Device_SensorDataReceived(object sender, SensorDataEventArgs e)
        {
            this.DeciveReadingUpdated?.Invoke(sender, e);
        }

        public ObservableCollection<SensorDataType> DataTypes { get; } = new ObservableCollection<SensorDataType>();

        public event EventHandler<SensorDataEventArgs> DeciveReadingUpdated;

        public override Task OnDisappearing()
        {
            device.SensorDataReceived -= Device_SensorDataReceived;
            return device.Disconnect();
        }
    }
}
