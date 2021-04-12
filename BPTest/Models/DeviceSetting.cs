using System.Windows.Input;
using Xamarin.Forms;

namespace BPTest.Models
{
    public class DeviceSetting
    {
        public static readonly string CATEGORY_TRASH = "TRASH";
        public static readonly string CATEGORY_STORAGE = "STORAGE";
        public static readonly string CATEGORY_CONNECTIVITY = "CONNECTIVITY";
        public static readonly string CATEGORY_DEVICE = "DEVICE";
        public string Category { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public ImageSource Icon { get; set; }
        public ICommand Command { get; set; }
    }
}
