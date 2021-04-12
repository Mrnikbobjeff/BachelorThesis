using System;
using System.Collections.Generic;
using System.Text;

namespace BPTest.Shared.Models.Views
{
    public class GroupedSetting : List<Setting>
    {
        public GroupedSetting(string name, IEnumerable<Setting> settings) : base(settings)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
