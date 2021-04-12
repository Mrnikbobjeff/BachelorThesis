using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BPTest.Shared.Models.Views
{
    public class AssignedDevice
    {
        public ImageSource Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PairingStatus { get; set; }
        public string ConnectionState { get; set; }
        public bool IsDeviceConnected { get; set; }
    }
}
