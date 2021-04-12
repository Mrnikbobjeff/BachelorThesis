using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BPTest.Shared.Devices
{
    public class DeviceType
    {
        private readonly IDeviceBuilder builder;
        public DeviceType(IDeviceBuilder builder)
        {
            this.builder = builder;
        }

        public string Name => builder.DeviceTypeName;
        public ImageSource Icon => builder.DeviceTypeIcon;

        public bool IsBluetooth => builder.IsBluetooth;
    }
}
