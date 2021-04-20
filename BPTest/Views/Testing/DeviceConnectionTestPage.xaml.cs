using Autofac;
using BPTest.ViewModels.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPTest.Views.Testing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceConnectionTestPage : ContentPage
    {
        public DeviceConnectionTestPage()
        {
            InitializeComponent();
            BindingContext = App.DependencyService.Resolve<EmpaticaConnectionTestPageViewModel>();
        }
    }
}