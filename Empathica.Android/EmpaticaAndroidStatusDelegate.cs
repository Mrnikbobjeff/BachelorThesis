using System;
using Android.App;
using Android.Runtime;
using BPTest.Shared.Devices;
using Com.Empatica.Empalink;
using Com.Empatica.Empalink.Config;
using Empathica.Shared;
using Empatica.Delegates;
using NLog;

namespace Empathica.Android
{
    public class EmpaticaAndroidStatusDelegate : Java.Lang.Object, IEmpaStatusDelegate, IEmpaticaConnectionHandler
    {
        private EmpaDeviceManager deviceManager = null;
        private bool isScanning = false;
        private EmpaStatus deviceStatus = null;

        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly EmpaticaAndroidDevice device;

        public EmpaticaAndroidStatusDelegate(EmpaticaAndroidDevice device)
        {
            this.device = device;
            deviceManager = new EmpaDeviceManager(
                 Application.Context,
                 new EmpaticaAndroidDataDelegate((x) => device.AddSensorData(x)),
                 this);
            deviceManager.AuthenticateWithAPIKey(EmpaticaSharedDevice.ApiKey);
        }

        public void BluetoothStateChanged()
        {
            logger.Debug("Undescribed function bluetoothStateChanged has been called");
        }

        public void DidDiscoverDevice(EmpaticaDevice device, string deviceLabel, int rssi, bool allowed)
        {
            if (allowed)
            {
                this.device.OnDeviceFound(device);
            }
        }

        public void DidEstablishConnection()
        {
            logger.Info($"Connection established to empatica");
        }

        public void DidFailedScanning(int errorCode)
        {
            logger.Info($"Connection established to empatica");
        }

        public void DidRequestEnableBluetooth()
        {
            logger.Error("Bluetooth is off!");
        }

        public void DidUpdateOnWristStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status)
        {
            logger.Debug($"On-Wrist status is now {status}");
        }

        public void DidUpdateSensorStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status, EmpaSensorType type)
        {
            logger.Info("Sensor status of $type changed to {status}");
        }

        private void StartScanning()
        {
            if (!isScanning)
            {
                deviceManager.StartScanning();
                isScanning = true;
            }
        }

        public void DidUpdateStatus(EmpaStatus status)
        {
            deviceStatus = status;
            if (status.Ordinal() == EmpaStatus.Ready.Ordinal())
            {
                StartScanning();
            }
            else if (status.Ordinal() == EmpaStatus.Connected.Ordinal())
            {
                device.OnCnnectionStatusChanged(DeviceStatus.CONNECTED);
            }
            else if (status.Ordinal() == EmpaStatus.Disconnected.Ordinal())
            {
                device.OnCnnectionStatusChanged(DeviceStatus.DISCONNECTED);
            }
            else if (status.Ordinal() == EmpaStatus.Connecting.Ordinal())
            {
                device.OnCnnectionStatusChanged(DeviceStatus.CONNECTING);
            }
            else
            {
                logger.Info($"Connection state of empatica: {status}");
            }
        }

        public void Connect(IEmpaticaDevice device)
        {
            if (deviceStatus.Ordinal() == EmpaStatus.Connected.Ordinal() ||
                deviceStatus.Ordinal() == EmpaStatus.Connecting.Ordinal() ||
                deviceStatus.Ordinal() == EmpaStatus.Disconnecting.Ordinal())
            {
                return;
            }

            var actualDevice = ((DiscoveredAndroidEmpatica)device).ActualDevice;
            try
            {
                deviceManager.ConnectDevice(actualDevice);
            }
            catch (ConnectionNotAllowedException)
            {
                logger.Info($"Not allowed to connect to ${actualDevice.AdvertisingName} ${actualDevice.HardwareId}");
            }
        }

        public void Disconnect()
        {
            deviceManager.Disconnect();
        }

        public void StopScanning()
        {
            deviceManager.StopScanning();
        }

        public void StartScanning(EmpaticaSharedDevice sharedDevice)
        {
            deviceManager.StartScanning();
        }
    }
}