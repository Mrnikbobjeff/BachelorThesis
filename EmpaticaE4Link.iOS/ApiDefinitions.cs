using System;
using Foundation;
using ObjCRuntime;

namespace EmpaticaBindingLibrary
{
	[Static]
	partial interface Constants
	{
		// extern double E4linkVersionNumber;
		[Field ("E4linkVersionNumber", "__Internal")]
		double E4linkVersionNumber { get; }
	}

	// @protocol EmpaticaDelegate <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface EmpaticaDelegate
	{
		// @required -(void)didUpdateBLEStatus:(BLEStatus)status;
		[Abstract]
		[Export ("didUpdateBLEStatus:")]
		void DidUpdateBLEStatus (BLEStatus status);

		// @required -(void)didDiscoverDevices:(NSArray *)devices;
		[Abstract]
		[Export ("didDiscoverDevices:")]
		void DidDiscoverDevices (NSObject[] devices);
	}

	// @protocol EmpaticaDeviceDelegate <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface EmpaticaDeviceDelegate
	{
		// @optional -(void)didReceiveGSR:(float)gsr withTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveGSR:withTimestamp:fromDevice:")]
		void DidReceiveGSR (float gsr, double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didReceiveBVP:(float)bvp withTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveBVP:withTimestamp:fromDevice:")]
		void DidReceiveBVP (float bvp, double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didReceiveTemperature:(float)temp withTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveTemperature:withTimestamp:fromDevice:")]
		void DidReceiveTemperature (float temp, double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didReceiveAccelerationX:(char)x y:(char)y z:(char)z withTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveAccelerationX:y:z:withTimestamp:fromDevice:")]
		void DidReceiveAccelerationX (sbyte x, sbyte y, sbyte z, double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didReceiveIBI:(float)ibi withTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveIBI:withTimestamp:fromDevice:")]
		void DidReceiveIBI (float ibi, double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didReceiveBatteryLevel:(float)level withTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveBatteryLevel:withTimestamp:fromDevice:")]
		void DidReceiveBatteryLevel (float level, double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didReceiveTagAtTimestamp:(double)timestamp fromDevice:(EmpaticaDeviceManager *)device;
		[Export ("didReceiveTagAtTimestamp:fromDevice:")]
		void DidReceiveTagAtTimestamp (double timestamp, EmpaticaDeviceManager device);

		// @optional -(void)didUpdateDeviceStatus:(DeviceStatus)status forDevice:(EmpaticaDeviceManager *)device;
		[Export ("didUpdateDeviceStatus:forDevice:")]
		void DidUpdateDeviceStatus (DeviceStatus status, EmpaticaDeviceManager device);

		// @optional -(void)didUpdateOnWristStatus:(SensorStatus)onWristStatus forDevice:(EmpaticaDeviceManager *)device;
		[Export ("didUpdateOnWristStatus:forDevice:")]
		void DidUpdateOnWristStatus (SensorStatus onWristStatus, EmpaticaDeviceManager device);
	}

	// @interface EmpaticaAPI : NSObject
	[BaseType (typeof(NSObject))]
	interface EmpaticaAPI
	{
		// +(void)authenticateWithAPIKey:(NSString *)apiKey andCompletionHandler:(void (^)(BOOL, NSString *))handler;
		[Static]
		[Export ("authenticateWithAPIKey:andCompletionHandler:")]
		void AuthenticateWithAPIKey (string apiKey, Action<bool, NSString> handler);

		// +(void)discoverDevicesWithDelegate:(id<EmpaticaDelegate>)empaticaDelegate;
		[Static]
		[Export ("discoverDevicesWithDelegate:")]
		void DiscoverDevicesWithDelegate (EmpaticaDelegate empaticaDelegate);

		// +(void)cancelDiscovery;
		[Static]
		[Export ("cancelDiscovery")]
		void CancelDiscovery ();

		// +(void)prepareForBackground;
		[Static]
		[Export ("prepareForBackground")]
		void PrepareForBackground ();

		// +(void)prepareForResume;
		[Static]
		[Export ("prepareForResume")]
		void PrepareForResume ();

		// +(BLEStatus)status;
		[Static]
		[Export ("status")]
		BLEStatus Status { get; }
	}

	// @interface EmpaticaDeviceManager : NSObject
	[BaseType (typeof(NSObject))]
	interface EmpaticaDeviceManager
	{
		// @property (readonly, copy, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * serialNumber;
		[Export ("serialNumber")]
		string SerialNumber { get; }

		// @property (readonly, copy, nonatomic) NSString * advertisingName;
		[Export ("advertisingName")]
		string AdvertisingName { get; }

		// @property (readonly, copy, nonatomic) NSString * hardwareId;
		[Export ("hardwareId")]
		string HardwareId { get; }

		// @property (readonly, copy, nonatomic) NSString * firmwareVersion;
		[Export ("firmwareVersion")]
		string FirmwareVersion { get; }

		// @property (readonly, assign, nonatomic) BOOL allowed;
		[Export ("allowed")]
		bool Allowed { get; }

		// @property (readonly, assign, nonatomic) BOOL isFaulty;
		[Export ("isFaulty")]
		bool IsFaulty { get; }

		// @property (readonly, assign, nonatomic) DeviceStatus deviceStatus;
		[Export ("deviceStatus", ArgumentSemantic.Assign)]
		DeviceStatus DeviceStatus { get; }

		// -(void)connectWithDeviceDelegate:(id<EmpaticaDeviceDelegate>)deviceDelegate;
		[Export ("connectWithDeviceDelegate:")]
		void ConnectWithDeviceDelegate (EmpaticaDeviceDelegate deviceDelegate);

		// -(void)connectWithDeviceDelegate:(id<EmpaticaDeviceDelegate>)deviceDelegate andConnectionOptions:(NSArray *)connectionOptions;
		[Export ("connectWithDeviceDelegate:andConnectionOptions:")]
		void ConnectWithDeviceDelegate (EmpaticaDeviceDelegate deviceDelegate, NSObject[] connectionOptions);

		// -(void)disconnect;
		[Export ("disconnect")]
		void Disconnect ();

		// -(void)cancelConnection;
		[Export ("cancelConnection")]
		void CancelConnection ();
	}
}