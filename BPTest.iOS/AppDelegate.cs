using Autofac;
using BPTest.Shared.Constants;
using BPTest.Shared.Devices;
using Empatica.iOS;
using EmpaticaBindingLibrary;
using Foundation;
using InternalSensors.iOS;
using UIKit;

namespace BPTest.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<InternaliOSDeviceBuilder>().Named<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.InternalDevice);
            containerBuilder.RegisterType<EmpaticaiOSDeviceBuilder>().Named<IDeviceBuilder>(PlatformSpecificDeviceBuilderServiceKeys.Empatica);
            LoadApplication(new App(containerBuilder));

            return base.FinishedLaunching(app, options);
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            EmpaticaAPI.PrepareForBackground();
            base.DidEnterBackground(uiApplication);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            EmpaticaAPI.PrepareForResume();
            base.OnActivated(uiApplication);
        }
    }
}
