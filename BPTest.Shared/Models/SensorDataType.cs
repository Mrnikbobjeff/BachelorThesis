namespace BPTest.Shared.Models
{
    public enum SensorDataType
    {   // includes gravity (~9.8m/s²)
        ACCELERATION_X,
        ACCELERATION_Y,
        ACCELERATION_Z,

        GYROSCOPE_X,
        GYROSCOPE_Y,
        GYROSCOPE_Z,

        MAGNETOMETER_X,
        MAGNETOMETER_Y,
        MAGNETOMETER_Z,

        ORIENTATION_W,
        ORIENTATION_X,
        ORIENTATION_Y,
        ORIENTATION_Z,

        AMBIENT_TEMPERATURE,

        PRESSURE,

        GRAVITY_X,
        GRAVITY_Y,
        GRAVITY_Z,

        // acceleration without gravity
        LINEAR_ACCELERATION_X,
        LINEAR_ACCELERATION_Y,
        LINEAR_ACCELERATION_Z,

        LIGHT,

        HEART_RATE,
        OXYGEN_SATURATION_SPO2,
        BLOOD_PERFUSION,
        STEPS,
        BLOOD_PULSE_WAVE,
        RESPIRATION_RATE,
        TEMPERATURE_LOCAL,
        GALVANIC_SKIN_RESPONSE,
        PERFUSION_INDEX,
        STRESS,
        ENERGY,
        RICHNESS,
        ACTIVITY_CLASSIFICATION,
        HEART_RATE_VARIABILITY,
        TEMPERATURE_BAROMETER,

        ECG,

        GPS_LATITUDE,
        GPS_LONGITUDE,

        TAG,
        INTER_BEAT_INTERVAL,
        BLOOD_VOLUME_PULSE,

        PPG,

        SCANNED_BLUETOOTH_DEVICES

    }
}
