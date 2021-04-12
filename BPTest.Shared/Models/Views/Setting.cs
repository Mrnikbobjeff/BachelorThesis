using System.Windows.Input;
using Xamarin.Forms;

namespace BPTest.Shared.Models.Views
{
    public class Setting
    {
        public string Key { get; set; }
        public ImageSource Icon { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public ICommand TappedCommand { get; set; }
    }
}
