using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Models
{
    public class SelectedSensors : RealmObject
    {
        public long Id { get; set; }

        public long DeviceId { get; set; }
        public int SensorDataType { get; set; }
    }
}
