using BPTest.Repositories.Interfaces;
using BPTest.Shared.Devices;
using BPTest.Shared.Models.Views;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTest.Repositories
{
    public class DeviceRepository : SensorhubRealmConfiguration, IDeviceRepository
    {
        public async Task<ActiveDevice[]> GetActiveDevices()
        {
            using (var realm = await Realm.GetInstanceAsync(Configuration))
            {
                return realm.All<Device>().ToArray().Select(x => new ActiveDevice { Id = x.Id, Module = x.Module, Adress = x.PairedDeviceAdress, Name = x.Name }).ToArray();
            }
        }

        public async Task UpdateDevices(ICollection<SensorHub.Backend.Device> activeDevices)
        {
            using (var realm = await Realm.GetInstanceAsync(Configuration))
            {
                var activeDeviceTypeIds = activeDevices.Select(x => x.DeviceType.Module).ToArray();
                var oldDevices = realm.All<Device>().Where(device => !activeDeviceTypeIds.Contains(device.Module));
                realm.RemoveRange(oldDevices);

                var newDevices = activeDevices.Where(x => !activeDeviceTypeIds.Contains(x.DeviceType.Module))
                    .Select(x => new Device { Module = x.DeviceType.Module, Name = x.Name, PairedDeviceDescription = x.DeviceType.Name });

                realm.Add(newDevices);
            }
        }
    }
}
