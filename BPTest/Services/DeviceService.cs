using BPTest.Repositories.Interfaces;
using BPTest.Services.Interfaces;
using BPTest.Shared.Models.Views;
using SensorHub.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTest.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ISwaggerClient swaggerClient;
        private readonly IDeviceRepository deviceRepository;

        public DeviceService(ISwaggerClient swaggerClient, IDeviceRepository deviceRepository)
        {
            this.swaggerClient = swaggerClient;
            this.deviceRepository = deviceRepository;
        }

        public async Task<ActiveDevice[]> GetActiveDevicesAsync(bool refresh = false)
        {
            if(refresh)
            {
                var activeDevices = await swaggerClient.GetDeviceActiveStudyAsync().ConfigureAwait(false);
                await deviceRepository.UpdateDevices(activeDevices).ConfigureAwait(false); ;
                return await deviceRepository.GetActiveDevices().ConfigureAwait(false); ;
            }
            else
            {
                return await deviceRepository.GetActiveDevices().ConfigureAwait(false); ;
            }
        }
    }
}
