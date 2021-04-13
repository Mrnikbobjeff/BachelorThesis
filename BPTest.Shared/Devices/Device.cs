using BPTest.Shared.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Devices
{
    public class Device : RealmObject
    {
        [PrimaryKey]
        public long Id { get; set; }
        public string Name { get; set; }

        public IList<SelectedSensors> SelectedSensors { get; }

        public string PairedDeviceAdress { get; set; }

        public string PairedDeviceDescription { get; set; }

        public string Module { get; set; }
    }
}
