using BPTest.Shared.Models.Views;
using SensorHub.Backend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BPTest.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<ActiveDevice[]> GetActiveDevices();
        Task UpdateDevices(ICollection<Device> activeDevices);
    }
}
