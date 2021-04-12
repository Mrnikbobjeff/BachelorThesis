using System;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Runtime;

namespace InternalSensors.Android
{
    public class Photometer
    {
        static SensorManager SensorManager => Application.Context.ApplicationContext.GetSystemService(Context.SensorService) as SensorManager;
        internal static bool IsSupported => SensorManager?.GetDefaultSensor(SensorType.Light)!= null;
        readonly Sensor luminositySensor;
        readonly ISensorEventListener listener;

        public event EventHandler<PhotometerEventArgs> ReadingChanged;

        public Photometer()
        {
            luminositySensor = SensorManager?.GetDefaultSensor(SensorType.Light);
        }

        public void Start() 
        {
            SensorManager.RegisterListener(listener, luminositySensor, SensorDelay.Normal);
        }

        public void Stop()
        {
            SensorManager.UnregisterListener(listener);
        }

        internal void OnChanged(float value)
        {
            ReadingChanged.Invoke(this, new PhotometerEventArgs { Value = value });
        }
    }

    public class PhotometerEventArgs : EventArgs
    {
        public double Value { get; set; }
    }

    class LightSensorListener : Java.Lang.Object, ISensorEventListener, IDisposable
    {
        private readonly Photometer photometer;

        public LightSensorListener(Photometer photometer)
        {
            this.photometer = photometer;
        }
        void ISensorEventListener.OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        void ISensorEventListener.OnSensorChanged(SensorEvent e)
        {
            photometer.OnChanged(e.Values[0]);
        }
    }
}