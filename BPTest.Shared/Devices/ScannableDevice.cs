namespace BPTest.Shared.Devices
{
    public abstract class ScannableDevice : PhysicalDevice
    {
        public abstract void OnCnnectionStatusChanged(DeviceStatus status);

        protected ScannableDevice(Device device) : base(device)
        {
        }
    }
}
