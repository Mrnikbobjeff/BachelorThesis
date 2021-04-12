using Realms;
using System;

namespace BPTest.Shared.Models
{
    public class SensorData : RealmObject
    {
        public long Id { get; set; }
        public int Type { get; set; }

        public DateTimeOffset Time { get; set; } 

        public double Accuracy { get; set; }
        public double Value { get; set; }
        public long DeviceId { get; set; }
    }
}
