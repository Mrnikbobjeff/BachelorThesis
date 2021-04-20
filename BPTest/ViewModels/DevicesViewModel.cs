using BPTest.Services.Interfaces;
using BPTest.Shared.Models.Views;
using BPTest.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BPTest.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
        private readonly IDeviceService deviceService;

        public ICommand RefreshUnpairedDevicesCommand { get; }
        public ICommand TestUnpairedDevicesCommand { get; }
        public static ICommand DeviceTappedCommand { get; private set; } = new Command(async (x) => await Shell.Current.GoToAsync($"devices/details?deviceId={(x as ActiveDevice).Id}"));
        public ObservableCollection<ActiveDevice> Devices { get; } = new ObservableCollection<ActiveDevice>();

        public DevicesViewModel(IDeviceService deviceService)
        {
            Title = "Devices";
            RefreshUnpairedDevicesCommand = new Command(async () => await RefreshDevices());
            TestUnpairedDevicesCommand = new Command(async () => await TestUnpairedDevices());
            this.deviceService = deviceService;
        }

        public override async Task OnAppearing()
        {
            Devices.Clear();
            Devices.AddRange(await deviceService.GetActiveDevicesAsync());
            if (!Devices.Any())
                await RefreshDevices();
        }

        public async Task RefreshDevices()
        {
            Devices.Clear();
            Devices.AddRange(await deviceService.GetActiveDevicesAsync(true));
        }

        public async Task TestUnpairedDevices()
        {
            await Shell.Current.GoToAsync("devices/testempatica");
        }
    }
}
