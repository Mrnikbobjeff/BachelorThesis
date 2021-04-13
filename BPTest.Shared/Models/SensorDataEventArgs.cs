using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Models
{
    public class SensorDataEventArgs : EventArgs
    {
        public SensorData Value { get; set; }
    }
}
