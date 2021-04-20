using BPTest.Repositories.Interfaces;
using BPTest.Shared.Devices;
using BPTest.Shared.Models.Views;
using BPTest.Shared.Repositories;
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
                var devices = realm.All<Device>().ToArray().Select(x => new ActiveDevice { Id = x.Id, Module = x.Module, Adress = x.PairedDeviceAdress, Name = x.Name }).ToArray();
                return devices;
            }
        }

        public async Task UpdateDevices(ICollection<SensorHub.Backend.Device> activeDevices)
        {
            using (var realm = await Realm.GetInstanceAsync(Configuration))
            {
                realm.Write(() =>
                {
                    realm.RemoveAll<Device>();

                    var newDevices = activeDevices
                        .Select(x => new Device { Module = x.DeviceType.Module, Name = x.Name, PairedDeviceDescription = x.DeviceType.Name, Id = x.Id }).ToArray();
                    realm.Add(newDevices);
                });
            }
        }
    }
}
