using Autofac;
using BPTest.Shared.Constants;
using BPTest.Shared.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Utils
{
    public interface IDeviceBuilderFactory
    {
        IDeviceBuilder ForDeviceModule(string module);
    }
    public class DeviceBuilderFactory : IDeviceBuilderFactory
    {
        public IDeviceBuilder ForDeviceModule(string module)
        {
            switch(module)
            {
                case "internal":
                    return App.DependencyService.ResolveNamed<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.InternalDevice);
                case "empatica":
                    return App.DependencyService.ResolveNamed<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.Empatica);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
