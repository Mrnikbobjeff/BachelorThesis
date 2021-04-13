using System;
using BPTest.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace BPTest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            Permissions.RequestAsync<Sensors>().GetAwaiter().GetResult();
            Permissions.RequestAsync<LocationWhenInUse>().GetAwaiter().GetResult();
            base.OnAppearing();
        }
    }
}
