using BPTest.Services.Interfaces;
using BPTest.Shared.Models;
using BPTest.Shared.Models.Views;
using BPTest.Shared.Repositories;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTest.Services
{
    public class RecordingService : IRecordingService
    {
        public bool IsRecording { get; }

        public async Task StartRecording()
        {
            using (var realm = await Realm.GetInstanceAsync(SensorhubRealmConfiguration.Configuration))
            {
                if (realm.All<Recording>().Where(x => x.IsRunning).Any())
                    return;
                realm.Add(new Recording { StartTime = DateTimeOffset.UtcNow, IsRunning = true });
            }
        }

        public async Task SetRecordingDescription(long id, string name)
        {
            using (var realm = await Realm.GetInstanceAsync(SensorhubRealmConfiguration.Configuration))
            {
                var activeRecording = realm.Find<Recording>(id);
                if (activeRecording == null)
                    return;
                activeRecording.Description = name;
            }
        }

        public async Task SetActiveRecordingDescription(string name)
        {
            using (var realm = await Realm.GetInstanceAsync(SensorhubRealmConfiguration.Configuration))
            {
                var activeRecording = realm.All<Recording>().Where(x => x.IsRunning).FirstOrDefault();
                if (activeRecording == null)
                    return;
                activeRecording.Description = name;
            }
        }

        public async Task StopRecording()
        {
            using (var realm = await Realm.GetInstanceAsync(SensorhubRealmConfiguration.Configuration))
            {
                var activeRecording = realm.All<Recording>().Where(x => x.IsRunning).FirstOrDefault();
                if (activeRecording == null)
                    return;
                activeRecording.IsRunning = false;
            }
        }

        public async Task<RecordingModel[]> GetRecordings()
        {
            using (var realm = await Realm.GetInstanceAsync(SensorhubRealmConfiguration.Configuration))
            {
                return realm.All<Recording>().Where(x => !x.IsRunning).ToArray().Select(x => new RecordingModel { Description = x.Description, Duration = x.EndTime - x.StartTime, StartTime = x.StartTime }).ToArray();
            }
        }
    }
}
