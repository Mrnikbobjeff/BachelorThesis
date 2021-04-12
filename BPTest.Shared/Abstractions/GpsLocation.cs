using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Abstractions
{
    public class GpsLocation
    {
        public GpsLocation(double accuracy, double longitude, double latitude)
        {
            Accuracy = accuracy;
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Accuracy { get; }
        public double Longitude { get; }
        public double Latitude { get; }
    }
}
