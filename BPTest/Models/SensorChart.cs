using BPTest.Shared.Models;
using Microcharts;

namespace BPTest.Models
{
    public class SensorChart
    {
        public SensorDataType DataType { get; set; }
        public string SensorDataType { get; set; }
        public LineChart Chart { get; set; }
    }
}
