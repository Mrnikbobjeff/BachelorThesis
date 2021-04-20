using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Autofac;
using BPTest.Shared.Devices;
using InternalSensors.Android;
using BPTest.Shared.Constants;
using Empathica.Android;
using Android;
using System;
using AndroidX.Core.App;

namespace BPTest.Droid
{
    [Activity(Label = "BPTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(BaseContext);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<AndroidDeviceBuilder>().Named<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.InternalDevice);
            containerBuilder.RegisterType<EmpaticaAndroidDeviceBuilder>().Named<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.Empatica);
            LoadApplication(new App(containerBuilder));

            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Bluetooth }, 22);
            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.BluetoothAdmin }, 23);
            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.BluetoothPrivileged }, 24);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}