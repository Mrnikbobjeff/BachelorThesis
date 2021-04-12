namespace Empathica.Shared
{
    public interface IEmpaticaConnectionHandler
    {
        void Connect(IEmpaticaDevice device);
        void Disconnect();
        void StopScanning();
        void StartScanning(EmpaticaSharedDevice sharedDevice);
    }
}
