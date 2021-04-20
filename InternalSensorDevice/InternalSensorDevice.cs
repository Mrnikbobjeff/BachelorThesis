using BPTest.Shared.Abstractions;
using BPTest.Shared.Devices;
using BPTest.Shared.Models;
using BPTest.Shared.Repositories;
using Realms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Device = BPTest.Shared.Devices.Device;

namespace InternalSensors
{
    public class InternalSensorDevice : LocalDevice
    {
        public InternalSensorDevice(Device device) : base(device)
        {
        }

        public override string Name => "Internal Sensor Device";

        public override ImageSource Icon => ImageSource.FromFile("");

        public override BPTest.Shared.Devices.DeviceType DeviceType => throw new System.NotImplementedException();

        public override Task Connect()
        {
            return StartCollection();
        }

        public override Task Disconnect()
        {
            return StopCollection();
        }

        public async Task StartCollection()
        {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Default);
            //Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;
            //Magnetometer.Start(SensorSpeed.Default);
            //Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            //Gyroscope.Start(SensorSpeed.Default);
            //Barometer.ReadingChanged += Barometer_ReadingChanged;
            //Barometer.Start(SensorSpeed.Default);
            //OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
            //OrientationSensor.Start(SensorSpeed.Default);
            await StartGpsListener();
            await StartLuxmeterListener();
        }

        protected virtual Task StartGpsListener() => Task.CompletedTask;
        protected virtual Task StopGpsListener() => Task.CompletedTask;

        protected virtual Task StartLuxmeterListener() => Task.CompletedTask;
        protected virtual Task StopLuxmeterListener() => Task.CompletedTask;



        protected void GpsSensor_ReadingChanged(GpsLocation location)
        {
            AddSensorData(new SensorData { Accuracy = location.Accuracy, Value = location.Longitude, Type = (int)SensorDataType.GPS_LONGITUDE });
        }

        private void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            AddSensorData(new SensorData { Value = e.Reading.Orientation.X, Type = (int)SensorDataType.ORIENTATION_X });
            AddSensorData(new SensorData { Value = e.Reading.Orientation.Y, Type = (int)SensorDataType.ORIENTATION_Y });
            AddSensorData(new SensorData { Value = e.Reading.Orientation.Z, Type = (int)SensorDataType.ORIENTATION_Z });
            AddSensorData(new SensorData { Value = e.Reading.Orientation.W, Type = (int)SensorDataType.ORIENTATION_W });
        }
        private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            AddSensorData(new SensorData { Value = e.Reading.PressureInHectopascals, Type = (int)SensorDataType.PRESSURE });
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            AddSensorData(new SensorData { Value = e.Reading.AngularVelocity.X, Type = (int)SensorDataType.GYROSCOPE_X });
            AddSensorData(new SensorData { Value = e.Reading.AngularVelocity.Y, Type = (int)SensorDataType.GYROSCOPE_Y });
            AddSensorData(new SensorData { Value = e.Reading.AngularVelocity.Z, Type = (int)SensorDataType.GYROSCOPE_Z });
        }

        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            AddSensorData(new SensorData { Value = e.Reading.MagneticField.X, Type = (int)SensorDataType.MAGNETOMETER_X });
            AddSensorData(new SensorData { Value = e.Reading.MagneticField.Y, Type = (int)SensorDataType.MAGNETOMETER_Y });
            AddSensorData(new SensorData { Value = e.Reading.MagneticField.Z, Type = (int)SensorDataType.MAGNETOMETER_Z });
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            AddSensorData(new SensorData { Value = e.Reading.Acceleration.X, Type = (int)SensorDataType.ACCELERATION_X });
            AddSensorData(new SensorData { Value = e.Reading.Acceleration.Y, Type = (int)SensorDataType.ACCELERATION_Y });
            AddSensorData(new SensorData { Value = e.Reading.Acceleration.Z, Type = (int)SensorDataType.ACCELERATION_Z });
        }

        public async Task StopCollection()
        {
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            //Magnetometer.ReadingChanged -= Magnetometer_ReadingChanged;
            //Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
            //Barometer.ReadingChanged -= Barometer_ReadingChanged;
            //OrientationSensor.ReadingChanged -= OrientationSensor_ReadingChanged;

            //OrientationSensor.Stop();
            Accelerometer.Stop();
            //Magnetometer.Stop();
            //Barometer.Stop();
            //Gyroscope.Stop();
            await StopGpsListener();
            await StopLuxmeterListener();
        }
    }

    public class InternalDeviceBuilder : IDeviceBuilder
    {
        public InternalDeviceBuilder()
        {
            if (Xamarin.Forms.Device.iOS == Xamarin.Forms.Device.RuntimePlatform)
                DefaultSelectedSensors.Remove(SensorDataType.LIGHT);
        }
        public string DeviceTypeName => "Smartphone Sensors";
        public ImageSource DeviceTypeIcon => ImageSource.FromFile("");
        public bool IsBluetooth => false;
        public Dictionary<SensorDataType, bool> DefaultSelectedSensors { get; } = new Dictionary<SensorDataType, bool>
        {
            { SensorDataType.ACCELERATION_X, true },
            { SensorDataType.ACCELERATION_Y, true },
            { SensorDataType.ACCELERATION_Z, true },
            { SensorDataType.MAGNETOMETER_X, true },
            { SensorDataType.MAGNETOMETER_Y, true },
            { SensorDataType.MAGNETOMETER_Z, true },
            { SensorDataType.AMBIENT_TEMPERATURE, true },
            { SensorDataType.PRESSURE, true },
            { SensorDataType.GYROSCOPE_X, true },
            { SensorDataType.GYROSCOPE_Y, true },
            { SensorDataType.GYROSCOPE_Z, true },
            { SensorDataType.GRAVITY_X, true },
            { SensorDataType.GRAVITY_Y, true },
            { SensorDataType.GRAVITY_Z, true },
            { SensorDataType.LINEAR_ACCELERATION_X, true },
            { SensorDataType.LINEAR_ACCELERATION_Y, true },
            { SensorDataType.LINEAR_ACCELERATION_Z, true },
            { SensorDataType.ORIENTATION_X, true },
            { SensorDataType.ORIENTATION_Y, true },
            { SensorDataType.ORIENTATION_Z, true },
            { SensorDataType.LIGHT, true },
            { SensorDataType.GPS_LONGITUDE, true },
            { SensorDataType.GPS_LATITUDE, true }
        };

        public virtual PhysicalDevice FromDevice(Device device)
        {
            return new InternalSensorDevice(device);
        }

        public bool IsDeviceSupported(ScanResult scanResult, BluetoothDevice bluetoothDevice)
        {
            return false;
        }
    }
}
