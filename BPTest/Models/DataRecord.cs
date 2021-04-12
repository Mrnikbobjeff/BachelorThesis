using System;

namespace BPTest.Models
{
    public class DataRecord : IFormattable
    {
        public int Recordings { get; set; }
        public int Tags { get; set; }
        public int DataPoints { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"Currently locally storing {Recordings} recordings, {Tags} tags and {DataPoints} data points.";
        }
    }
}
