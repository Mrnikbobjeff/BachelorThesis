using BPTest.Shared.Devices;
using Empathica.Shared;

namespace Empatica.iOS
{
    public class EmpaticaiOSDevice : EmpaticaSharedDevice
    {
        public EmpaticaiOSDevice(Device device) : base(device)
        {
            connectionHandler = new EmpaticaiOSConnectionHandler(this, device);
        }
    }
    public class EmpaticaiOSDeviceBuilder : EmpaticaDeviceBuilder
    {
        public override PhysicalDevice FromDevice(Device device)
        {
            return new EmpaticaiOSDevice(device);
        }
    }
}