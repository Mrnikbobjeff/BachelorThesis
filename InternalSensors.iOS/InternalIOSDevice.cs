using BPTest.Shared.Abstractions;
using BPTest.Shared.Devices;
using CoreLocation;
using System.Threading.Tasks;

namespace InternalSensors.iOS
{
    public sealed class InternalIOSDevice : InternalSensorDevice
    {
        CLLocationManager locationManager = new CLLocationManager();

        public InternalIOSDevice(Device device) : base(device)
        {
        }

        protected override Task StartGpsListener()
        {
            locationManager.StartUpdatingLocation();
            locationManager.UpdatedLocation += LocationManager_UpdatedLocation;
            return Task.CompletedTask;
        }

        private void LocationManager_UpdatedLocation(object sender, CLLocationUpdatedEventArgs e)
        {
            GpsSensor_ReadingChanged(new GpsLocation(e.NewLocation.CourseAccuracy, e.NewLocation.Coordinate.Longitude, e.NewLocation.Coordinate.Latitude));
        }

        protected override Task StopGpsListener()
        {
            locationManager.StopUpdatingLocation();
            locationManager.UpdatedLocation -= LocationManager_UpdatedLocation;
            return Task.CompletedTask;
        }
    }
    public class InternaliOSDeviceBuilder : InternalDeviceBuilder
    {
        public override PhysicalDevice FromDevice(Device device)
        {
            return new InternalIOSDevice(device);
        }
    }
}