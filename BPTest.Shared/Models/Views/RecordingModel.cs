using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Models.Views
{
    public class RecordingModel
    {
        public string Description { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
