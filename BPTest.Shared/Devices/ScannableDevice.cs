namespace BPTest.Shared.Devices
{
    public abstract class ScannableDevice : PhysicalDevice
    {
        public virtual void OnCnnectionStatusChanged(DeviceStatus status)
        {
            Status = status;
        }

        protected ScannableDevice(Device device) : base(device)
        {
        }
    }
}
