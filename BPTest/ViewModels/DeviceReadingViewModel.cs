using BPTest.Repositories;
using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using BPTest.Shared.Repositories;
using BPTest.Utils;
using Realms;
using System;
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

        public DeviceReadingViewModel(IDeviceBuilderFactory deviceBuilderFactory)
        {
            this.deviceBuilderFactory = deviceBuilderFactory;
        }

        public void LoadDevice(int id)
        {
            var now = DateTimeOffset.Now;
            var allOfType = realm.All<SensorData>().Where(x => x.DeviceId == id && x.Time > now);
            DataTypes.Clear();
            var savedDevice = realm.All<Device>().Where(x => x.Id == id).Single();
            var deviceBuilder = deviceBuilderFactory.ForDeviceModule(savedDevice.Module);
            device = deviceBuilder.FromDevice(savedDevice);
            DataTypes.AddRange(deviceBuilder.DefaultSelectedSensors.Keys);
            allOfType.AsRealmCollection().CollectionChanged += DeiveReadingPageViewModel_CollectionChanged;
            device.Connect().GetAwaiter().GetResult() ;
        }

        private void DeiveReadingPageViewModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
                DeciveReadingUpdated.Invoke(this, item as SensorData);
        }

        public ObservableCollection<SensorDataType> DataTypes { get; } = new ObservableCollection<SensorDataType>();

        public event EventHandler<SensorData> DeciveReadingUpdated;

        public override Task OnDisappearing()
        {
            return device.Disconnect();
        }
    }
}
