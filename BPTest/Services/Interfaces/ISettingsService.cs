using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BPTest.Services.Interfaces
{
    public interface ISettingsService
    {
        Subject<string> SettingChanged { get; }
        Task<T> Get<T>(string key, T defaultValue);
        Task Save<T>(string key, T value);
        Task Delete(string value);
    }
}
