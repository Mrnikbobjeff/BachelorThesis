namespace EmpaticaBindingLibrary
{
	public enum BLEStatus : uint
	{
		NotAvailable,
		Ready,
		Scanning
	}

	public enum DeviceStatus : uint
	{
		Disconnected,
		Connecting,
		Connected,
		FailedToConnect,
		Disconnecting
	}

	public enum SensorStatus : uint
	{
		NotOnWrist,
		OnWrist,
		Dead
	}
}
