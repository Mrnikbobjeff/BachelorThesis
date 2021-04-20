using System;
using Android.Runtime;
using BPTest.Shared.Models;
using Com.Empatica.Empalink;
using Com.Empatica.Empalink.Config;
using Empatica.Delegates;

namespace Empathica.Android
{
    public class EmpaticaAndroidDataDelegate : Java.Lang.Object, IEmpaDataDelegate, IEmpaStatusDelegate
    {
        private readonly Action<SensorData> processData;

        public EmpaticaAndroidDataDelegate(Action<SensorData> processData)
        {
            this.processData = processData;
        }

        SensorData CreateNew(SensorDataType type, double value) => new SensorData { Value = value, Type = (int)  type };

        public void DidReceiveAcceleration(int x, int y, int z, double timestamp)
        {
            processData(CreateNew(SensorDataType.ACCELERATION_X, x));
            processData(CreateNew(SensorDataType.ACCELERATION_Y, y));
            processData(CreateNew(SensorDataType.ACCELERATION_Z, z));
        }

        public void DidReceiveBatteryLevel(float level, double timestamp)
        {
        }

        public void DidReceiveBVP(float bvp, double timestamp)
        {
            processData(CreateNew(SensorDataType.BLOOD_VOLUME_PULSE, bvp));
        }

        public void DidReceiveGSR(float gsr, double timestamp)
        {
            processData(CreateNew(SensorDataType.GALVANIC_SKIN_RESPONSE, gsr));
        }

        public void DidReceiveIBI(float ibi, double timestamp)
        {
            processData(CreateNew(SensorDataType.INTER_BEAT_INTERVAL, ibi));
        }

        public void DidReceiveTag(double timestamp)
        {
        }

        public void DidReceiveTemperature(float t, double timestamp)
        {
            processData(CreateNew(SensorDataType.TEMPERATURE_LOCAL, t));
        }

        public void BluetoothStateChanged()
        {
        }

        public void DidDiscoverDevice(EmpaticaDevice device, string deviceLabel, int rssi, bool allowed)
        {
        }

        public void DidEstablishConnection()
        {
        }

        public void DidFailedScanning(int errorCode)
        {
        }

        public void DidRequestEnableBluetooth()
        {
        }

        public void DidUpdateSensorStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status, EmpaSensorType type)
        {
        }

        public void DidUpdateStatus(EmpaStatus status)
        {
        }

        public void DidUpdateOnWristStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status)
        {
        }
    }
}