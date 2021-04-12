using BPTest.Shared.Models;
using BPTest.Shared.Models.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BPTest.Services.Interfaces
{
    public interface IRecordingService
    {

         bool IsRecording { get; }

         Task StartRecording();

         Task SetRecordingDescription(long id, string name);
         Task SetActiveRecordingDescription(string name);

         Task StopRecording();

        Task<RecordingModel[]> GetRecordings();
    }
}
