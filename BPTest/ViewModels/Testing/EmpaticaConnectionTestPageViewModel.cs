using Autofac;
using BPTest.Shared.Constants;
using BPTest.Shared.Devices;
using BPTest.Utils;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Device = BPTest.Shared.Devices.Device;

namespace BPTest.ViewModels.Testing
{
    public class EmpaticaConnectionTestPageViewModel : BaseViewModel
    {
        private string connectionState;
        PhysicalDevice physicalDevice;
        IDeviceBuilder empaticaDeviceBuilder;
        private bool reached;
        private string deviceId = "10";

        public ICommand ConnectCommand { get; set; }
        public ICommand DisconnectCommand { get; set; }
        public ICommand LoadDevice { get; set; }
        public ICommand LoadState { get; set; }
        public string DeviceId { get => deviceId; set => SetProperty(ref deviceId, value); }
        public string ConnectionState { get => connectionState; set => SetProperty(ref connectionState, value); }
        public EmpaticaConnectionTestPageViewModel()
        {
            empaticaDeviceBuilder = App.DependencyService.ResolveNamed<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.Empatica);

            ConnectCommand = new Command(() => { physicalDevice.Connect(); });
            LoadState = new Command(() => { ConnectionState = physicalDevice.Status.ToString(); });
            DisconnectCommand = new Command(() => { physicalDevice.Disconnect(); });
            LoadDevice = new Command(() => { LoadDeviceImplementation(); });
        }

        void LoadDeviceImplementation()
        {
            using (var realm = Realm.GetInstance())
            {
                var devices = realm.All<Device>().ToArray();
                var device = realm.Find<Device>(long.Parse(DeviceId));
                device = device.Clone();
                physicalDevice = empaticaDeviceBuilder.FromDevice(device);
                physicalDevice.SensorDataReceived += PhysicalDevice_SensorDataReceived;
                physicalDevice.ConnectionStatusChanged += PhysicalDevice_ConnectionStatusChanged;
            }
        }

        private void PhysicalDevice_ConnectionStatusChanged(object sender, EventArgs e)
        {
            ConnectionState = physicalDevice.Status.ToString();
        }

        public override Task OnDisappearing()
        {
            physicalDevice.SensorDataReceived -= PhysicalDevice_SensorDataReceived;
            return base.OnDisappearing();
        }

        private void PhysicalDevice_SensorDataReceived(object sender, Shared.Models.SensorDataEventArgs e)
        {
        }
    }
}
