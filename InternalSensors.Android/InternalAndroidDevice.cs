using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using BPTest.Shared.Abstractions;
using BPTest.Shared.Devices;
using Xamarin.Essentials;

namespace InternalSensors.Android
{
    public class InternalAndroidDevice : InternalSensorDevice
    {
        LocationProvider locationProvider;
        Photometer photometer = new Photometer();

        public InternalAndroidDevice(Device device) : base(device)
        {
            locationProvider = new LocationProvider(new LocationCallback((e) => GpsSensor_ReadingChanged(e)));
        }

        protected override async Task StartGpsListener()
        {
            await locationProvider.StartLocationUpdate();
        }

        protected override Task StopGpsListener()
        {
            locationProvider.StopLocationUpdates();
            return Task.CompletedTask;
        }

        protected override Task StartLuxmeterListener()
        {
            photometer.Start();
            return Task.CompletedTask;
        }

        protected override Task StopLuxmeterListener()
        {
            photometer.Stop();
            return Task.CompletedTask;
        }
    }


    public class LocationCallback : LocationCallbackBase
    {
        private readonly Action<GpsLocation> onReadingReceived;

        public LocationCallback(Action<GpsLocation> onReadingReceived)
        {
            this.onReadingReceived = onReadingReceived;
        }
        public override void OnLocationResult(LocationResult p0)
        {
            onReadingReceived(new GpsLocation(p0.LastLocation.Accuracy, p0.LastLocation.Longitude, p0.LastLocation.Latitude));
            base.OnLocationResult(p0);
        }
    }

    public class LocationProvider
    {
        const int UPDATE_INTERVAL = 20 * 1000;
        private readonly LocationCallback callback;
        private FusedLocationProviderClient mFusedLocationClient;

        public LocationProvider(LocationCallback callback)
        {
            mFusedLocationClient = LocationServices.GetFusedLocationProviderClient(Application.Context);
            this.callback = callback;
        }

        public async Task StartLocationUpdate()
        {
            if (IsLocationEnabled())
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
                if (status == PermissionStatus.Granted)
                {
                    var mLocationRequest = LocationRequest.Create();
                    mLocationRequest.SetInterval(UPDATE_INTERVAL);
                    mLocationRequest.SetFastestInterval(UPDATE_INTERVAL);
                    mLocationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
                    mFusedLocationClient = LocationServices.GetFusedLocationProviderClient(Application.Context);
                    mFusedLocationClient.RequestLocationUpdates(
                        mLocationRequest, callback,
                        Looper.MainLooper
                    );
                }
            }
        }

        public void StopLocationUpdates()
        {
            mFusedLocationClient.RemoveLocationUpdates(callback);
        }

        private bool IsLocationEnabled()
        {
            var locationManager = Application.Context.GetSystemService(Context.LocationService) as LocationManager;
            return locationManager.IsProviderEnabled(LocationManager.NetworkProvider) || locationManager.IsProviderEnabled(LocationManager.GpsProvider);
        }
    }

    public class AndroidDeviceBuilder : InternalDeviceBuilder
    {
        public override PhysicalDevice FromDevice(Device device)
        {
            return new InternalAndroidDevice(device);
        }
    }
}