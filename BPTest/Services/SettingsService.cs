using BPTest.Services.Interfaces;
using BPTest.Shared.Constants;
using Newtonsoft.Json;
using NLog;
using System;
using System.Globalization;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BPTest.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(Action<string> backendUrlChangeNotification)
        {
            this.backendUrlChangeNotification = backendUrlChangeNotification;
        }
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Action<string> backendUrlChangeNotification;

        public Subject<string> SettingChanged { get; } = new Subject<string>();

        public Task<T> Get<T>(string key, T defaultValue)
        {
            switch(defaultValue)
            {
                case int _:
                    return GetStoredValue(key, defaultValue, x => (T) (object)int.Parse(x));
                case DateTimeOffset _:
                    return GetStoredValue(key, defaultValue, x => (T)(object)DateTimeOffset.Parse(x));
                case string _:
                    return GetStoredValue(key, defaultValue, x => (T)(object)x);
                default:
                    return GetStoredValue(key, defaultValue, x => JsonConvert.DeserializeObject<T>(x));
            }
        }

        async Task<T> GetStoredValue<T>(string key, T defaultValue, Func<string, T> castingFunction)
        {
            try
            {
                var value = await SecureStorage.GetAsync(key).ConfigureAwait(false);
                if (string.IsNullOrEmpty(value))
                    return defaultValue;
                return castingFunction(value);
            }
            catch(Exception ex)
            {
                logger.Warn(ex, "Failed to retrieve stored setting.");
            }
            return defaultValue;
        }

        async Task SetStoredValue(string key, string value)
        {
            try
            {
                await SecureStorage.SetAsync(key, value).ConfigureAwait(false);
                SettingChanged.OnNext(key);
                if (key == SettingsKeys.HostnameKey)
                    backendUrlChangeNotification(value);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, $"Failed to set setting ({key}).");
            }
        }

        public Task Save<T>(string key, T value)
        {
            switch (value)
            {
                case int integer:
                    return SetStoredValue(key, integer.ToString());
                case DateTimeOffset offset:
                    return SetStoredValue(key, offset.ToString(CultureInfo.InvariantCulture));
                case string str:
                    return SetStoredValue(key, str);
                default:
                    return SetStoredValue(key, JsonConvert.SerializeObject(value));
            }
        }

        public Task Delete(string value)
        {
            SecureStorage.Remove(value);
            return Task.CompletedTask;
        }
    }
}
