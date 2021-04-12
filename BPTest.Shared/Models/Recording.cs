using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Models
{
    public class Recording : RealmObject
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool IsRunning { get; set; }
    }
}
