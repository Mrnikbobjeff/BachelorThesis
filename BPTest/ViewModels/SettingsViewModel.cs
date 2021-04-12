using BPTest.Models;
using BPTest.Services.Interfaces;
using BPTest.Shared.Constants;
using BPTest.Shared.Models.Views;
using BPTest.Views.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BPTest.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        readonly ISettingsService settingsService;

        public IEnumerable<GroupedSetting> GroupedSettings { get; }

        public SettingsViewModel(ISettingsService settingsService)
        {
            Title = "Settings";
            this.settingsService = settingsService;
            var settings = new Setting[]
        {
            new Setting { Icon = "webhost.png", Title = "Hostname", Value = "www.test.de", TappedCommand = new Command(async () => await OnHostnameTapped()), Category = DeviceSetting.CATEGORY_CONNECTIVITY },
            new Setting { Icon = "devices.png", Title = "Device Modules", Value = "Manage device modules on this phone", Category = DeviceSetting.CATEGORY_DEVICE },
            new Setting { Icon = "storage.png", Title = "Database Size", Value = "1024 MB", TappedCommand = new Command(async () => await OnDatabaseSizeTapped()),  Category = DeviceSetting.CATEGORY_STORAGE },
            new Setting { Icon = "trashcan.png", Title = "Delete all data", Value = new DataRecord().ToString("", null),  TappedCommand = new Command(async () => { await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new DeleteAllDataPopupPage()); }), Category = DeviceSetting.CATEGORY_STORAGE }
        };
            GroupedSettings = settings.GroupBy(x => x.Category).Select(z => new GroupedSetting(z.Key, z));
        }


        public async Task OnDatabaseSizeTapped()
        {
            var databaseSize = await Shell.Current.DisplayPromptAsync("Enter database size (MB)", "Decide how much space data recorded by this app should occupy.", initialValue: "1024");
            if (int.TryParse(databaseSize, out var result) && result > 1024)
            {
                await settingsService.Save(SettingsKeys.DatabaseSizeKey, int.Parse(databaseSize)).ConfigureAwait(false);
            }
        }

        public async Task OnHostnameTapped()
        {
            var newHostname = await Shell.Current.DisplayPromptAsync("Enter Hostname", "Update the hostname which will be receiving your sensor data.", initialValue: "www.test.de");
            if (Uri.IsWellFormedUriString(newHostname, UriKind.Absolute))
            {
                await settingsService.Save(SettingsKeys.HostnameKey, newHostname).ConfigureAwait(false);
            }
        }

        public async Task DeleteAllData()
        {

        }
    }
}
