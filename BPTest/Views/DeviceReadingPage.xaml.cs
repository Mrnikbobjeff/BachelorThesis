using BPTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using Microcharts.Forms;
using BPTest.Models;
using BPTest.Shared.Models;
using Autofac;

namespace BPTest.Views
{
    [QueryProperty(nameof(DeviceId), "deviceId")]
    public partial class DeviceReadingPage : ContentPage
    {
        public string DeviceId
        {
            set
            {
                UpdatePageWithDeviceCharts(int.Parse(value));
            }
        }

        void UpdatePageWithDeviceCharts(int deviceId)
        {
            ViewModel.LoadDevice(deviceId);
            foreach(var type in ViewModel.DataTypes)
            {
                ViewModel.Charts.Add(new SensorChart { Chart = new LineChart { Entries = new ChartEntry[0], MinValue = 0 }, DataType = type, SensorDataType = type.ToString() }); ;
                viewDictionary.Add(type, ViewModel.Charts.First(x => x.DataType == type).Chart);
            }
        }

        DeviceReadingViewModel ViewModel = null;
        readonly Dictionary<SensorDataType, LineChart> viewDictionary = new Dictionary<SensorDataType, LineChart>();
        public DeviceReadingPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = App.DependencyService.Resolve<DeviceReadingViewModel>();
            ViewModel.DeciveReadingUpdated += ViewModel_DeciveReadingUpdated;
        }

        private void ViewModel_DeciveReadingUpdated(object sender, SensorDataEventArgs e)
        {
            if (viewDictionary.TryGetValue((SensorDataType)e.Value.Type, out var value))
            {
                value.Entries = value.Entries.Concat(new ChartEntry[] { new ChartEntry((float)e.Value.Value) });
            }
            else
            {

            }
        }

        protected override void OnDisappearing()
        {
            ViewModel.DeciveReadingUpdated -= ViewModel_DeciveReadingUpdated;
            ViewModel.OnDisappearing();
            base.OnDisappearing();
        }
    }
}