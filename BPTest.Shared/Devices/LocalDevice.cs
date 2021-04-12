using System;

namespace BPTest.Shared.Devices
{
    public abstract class LocalDevice : PhysicalDevice
    {
        protected LocalDevice(Device device) : base(device)
        {
        }

        public override void Disconnect() { }
        public override void Connect() { }

    }
}
