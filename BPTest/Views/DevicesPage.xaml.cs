using Autofac;
using BPTest.ViewModels;
using Xamarin.Forms;

namespace BPTest.Views
{
    public partial class DevicesPage : ContentPage
    {
        DevicesViewModel ViewModel = null;
        public DevicesPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = App.DependencyService.Resolve<DevicesViewModel>();
        }

        protected override async void OnAppearing()
        {
            await ViewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}