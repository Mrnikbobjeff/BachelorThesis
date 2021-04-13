using BPTest.Shared.Models;
using BPTest.Shared.Models.Views;
using BPTest.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace BPTest.ViewModels
{
    public class RecordingsViewModel : BaseViewModel
    {
        public List<Recording> Recordings { get; set; } = new List<Recording>();

        public ImageSource StartIcon { get; }
        public ImageSource PauseIcon { get; }
        public ImageSource StopIcon { get; }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand PauseCommand { get; }

        public ICommand ChangeDescription { get; set; }

        public RecordingsViewModel()
        {
            Title = "Recordings";
        }
    }
}
