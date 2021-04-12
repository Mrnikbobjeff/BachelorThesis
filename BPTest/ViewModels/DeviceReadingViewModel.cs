using BPTest.Repositories;
using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using BPTest.Utils;
using Realms;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BPTest.ViewModels
{
    public class DeviceReadingViewModel : BaseViewModel
    {
        Realm realm = Realm.GetInstance(new SensorhubRealmConfiguration().Configuration);
        public void LoadDevice(int id)
        {
            var now = DateTimeOffset.Now;
            var allOfType = realm.All<SensorData>().Where(x => x.DeviceId == id && x.Time > now);
            DataTypes.Clear();
            DataTypes.AddRange(realm.All<Device>().Where(x => x.Id == id).Single().SelectedSensors.Select(x => (SensorDataType)x.SensorDataType));
            allOfType.AsRealmCollection().CollectionChanged += DeiveReadingPageViewModel_CollectionChanged;
        }

        private void DeiveReadingPageViewModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
                DeciveReadingUpdated.Invoke(this, item as SensorData);
        }

        public ObservableCollection<SensorDataType> DataTypes { get; }

        public event EventHandler<SensorData> DeciveReadingUpdated;
    }
}
