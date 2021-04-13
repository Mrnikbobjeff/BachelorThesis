using System;
using System.Threading.Tasks;

namespace BPTest.Shared.Devices
{
    public abstract class LocalDevice : PhysicalDevice
    {
        protected LocalDevice(Device device) : base(device)
        {
        }

        public override Task Disconnect() => Task.CompletedTask;
        public override Task Connect() => Task.CompletedTask;

    }
}
