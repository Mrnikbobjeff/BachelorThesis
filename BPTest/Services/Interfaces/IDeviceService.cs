using BPTest.Shared.Models.Views;
using SensorHub.Backend;
using System.Threading.Tasks;

namespace BPTest.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<ActiveDevice[]> GetActiveDevicesAsync(bool refresh = false);
    }
}
