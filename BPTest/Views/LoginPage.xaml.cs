using Autofac;
using BPTest.Services;
using BPTest.ViewModels;
using SensorHub.Backend;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel ViewModel = null;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = App.DependencyService.Resolve<LoginViewModel>();
        }

        protected override async void OnAppearing()
        {
            await ViewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}