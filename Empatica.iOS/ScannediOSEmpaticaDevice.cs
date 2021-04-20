using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Empathica.Shared;
using EmpaticaBindingLibrary;
using Foundation;
using UIKit;

namespace Empatica.iOS
{
    public class ScannediOSEmpaticaDevice : IEmpaticaDevice
    {
        public ScannediOSEmpaticaDevice(EmpaticaDeviceManager manager)
        {
            Manager = manager;
        }
        public string Adress => Manager.AdvertisingName;

        public EmpaticaDeviceManager Manager { get; }

        public string Name => Manager.HardwareId;
    }
}