using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BPTest.Shared.Models;
using EmpaticaBindingLibrary;
using Foundation;
using NLog;
using UIKit;

namespace Empatica.iOS
{
    public class EmpaticaiOSDeviceDelegate : EmpaticaDeviceDelegate
    {
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Action<SensorData> addSensorData;

        public EmpaticaiOSDeviceDelegate(Action<SensorData> addSensorData)
        {
            this.addSensorData = addSensorData;
        }

        SensorData CreateNew(double value, SensorDataType type, DateTimeOffset timeStamp)
        {
            return new SensorData { Value = value, Time = timeStamp, Type = (int)type };
        }

        public override void DidReceiveAccelerationX(sbyte x, sbyte y, sbyte z, double timestamp, EmpaticaDeviceManager device)
        {
            addSensorData(CreateNew(x, SensorDataType.ACCELERATION_X, DateTimeOffset.UtcNow));
            addSensorData(CreateNew(y, SensorDataType.ACCELERATION_Y, DateTimeOffset.UtcNow));
            addSensorData(CreateNew(z, SensorDataType.ACCELERATION_Z, DateTimeOffset.UtcNow));
            base.DidReceiveAccelerationX(x, y, z, timestamp, device);
        }

        public override void DidReceiveBatteryLevel(float level, double timestamp, EmpaticaDeviceManager device)
        {
            base.DidReceiveBatteryLevel(level, timestamp, device);
        }

        public override void DidReceiveBVP(float bvp, double timestamp, EmpaticaDeviceManager device)
        {
            addSensorData(CreateNew(bvp, SensorDataType.BLOOD_VOLUME_PULSE, DateTimeOffset.UtcNow));
            base.DidReceiveBVP(bvp, timestamp, device);
        }

        public override void DidReceiveGSR(float gsr, double timestamp, EmpaticaDeviceManager device)
        {
            addSensorData(CreateNew(gsr, SensorDataType.GALVANIC_SKIN_RESPONSE, DateTimeOffset.UtcNow));
            base.DidReceiveGSR(gsr, timestamp, device);
        }

        public override void DidReceiveIBI(float ibi, double timestamp, EmpaticaDeviceManager device)
        {
            addSensorData(CreateNew(ibi, SensorDataType.INTER_BEAT_INTERVAL, DateTimeOffset.UtcNow));
            base.DidReceiveIBI(ibi, timestamp, device);
        }

        public override void DidReceiveTagAtTimestamp(double timestamp, EmpaticaDeviceManager device)
        {
            base.DidReceiveTagAtTimestamp(timestamp, device);
        }

        public override void DidReceiveTemperature(float temp, double timestamp, EmpaticaDeviceManager device)
        {
            addSensorData(CreateNew(temp, SensorDataType.TEMPERATURE_LOCAL, DateTimeOffset.UtcNow));
            base.DidReceiveTemperature(temp, timestamp, device);
        }

        public override void DidUpdateDeviceStatus(DeviceStatus status, EmpaticaDeviceManager device)
        {
            logger.Info($"DeviceStatus updated of device {device.HardwareId}: {status}");
            base.DidUpdateDeviceStatus(status, device);
        }

        public override void DidUpdateOnWristStatus(SensorStatus onWristStatus, EmpaticaDeviceManager device)
        {
            base.DidUpdateOnWristStatus(onWristStatus, device);
        }
    }
}