using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BPTest.Shared.Models.Views
{
    public class ActiveDevice
    {
        public long Id { get; set; }
        public Lazy<ImageSource> Icon { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string Adress { get; set; }
    }
}
