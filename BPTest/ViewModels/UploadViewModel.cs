using BPTest.Models;
using BPTest.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace BPTest.ViewModels
{
    public class UploadViewModel : BaseViewModel
    {
        public ICommand UpdateDataCommand { get; }

        DataRecord currentRecord;

        public DataRecord Record { get => currentRecord; set => SetProperty(ref currentRecord, value); }

        public UploadViewModel()
        {
            Title = "Upload";
            UpdateDataCommand = new Command(() => { });
        }
    }
}
