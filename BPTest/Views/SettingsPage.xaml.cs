using Autofac;
using BPTest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = App.DependencyService.Resolve<SettingsViewModel>();
        }
    }
}