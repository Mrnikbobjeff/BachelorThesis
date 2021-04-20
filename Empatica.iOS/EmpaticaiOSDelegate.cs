using System.Linq;

using Foundation;
using EmpaticaBindingLibrary;
using Empathica.Shared;
using NLog;

namespace Empatica.iOS
{
    public class EmpaticaiOSDelegate : EmpaticaDelegate
    {
        private readonly EmpaticaSharedDevice device;
        readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EmpaticaiOSDelegate(EmpaticaSharedDevice device)
        {
            this.device = device;
        }
        public override void DidDiscoverDevices(NSObject[] devices)
        {
            var managers = devices.Cast<EmpaticaDeviceManager>();
            foreach (var manager in managers)
            {
                if (manager.Allowed)
                    this.device.OnDeviceFound(new ScannediOSEmpaticaDevice(manager));
            }
        }

        public override void DidUpdateBLEStatus(BLEStatus status)
        {
            logger.Info($"BLE Status updated: {status}");
        }
    }
}