using Realms;

namespace BPTest.Shared.Repositories
{
    public class SensorhubRealmConfiguration
    {
        RealmConfiguration _configuration;

        public static RealmConfiguration Configuration => _configuration ?? (_configuration = new RealmConfiguration());
    }
}
