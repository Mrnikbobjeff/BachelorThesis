﻿using BPTest.ViewModels;
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
            foreach (var item in ViewModel.DataTypes)
            {
                var view = new ChartView();
                var stackLayout = new StackLayout();
                view.HorizontalOptions = LayoutOptions.CenterAndExpand;
                view.VerticalOptions = LayoutOptions.CenterAndExpand;
                view.BackgroundColor = Color.Blue;
                view.WidthRequest = 200;
                view.HeightRequest = 200;
                view.Chart = new LineChart();
                stackLayout.Children.Add(view);
                viewDictionary.Add(item, view);
                Content = stackLayout;
            }
        }
        DeviceReadingViewModel ViewModel = null;
        readonly Dictionary<SensorDataType, ChartView> viewDictionary = new Dictionary<SensorDataType, ChartView>();
        public DeviceReadingPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = App.DependencyService.Resolve<DeviceReadingViewModel>();
            ViewModel.DeciveReadingUpdated += ViewModel_DeciveReadingUpdated;
        }

        private void ViewModel_DeciveReadingUpdated(object sender, SensorData e)
        {
            if (viewDictionary.TryGetValue((SensorDataType)e.Type, out var value))
            {
                value.Chart.Entries = value.Chart.Entries.Concat(new ChartEntry[] { new ChartEntry((float)e.Value) });
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