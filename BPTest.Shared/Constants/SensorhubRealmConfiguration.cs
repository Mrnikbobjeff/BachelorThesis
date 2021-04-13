using Realms;

namespace BPTest.Shared.Repositories
{
    public class SensorhubRealmConfiguration
    {
        static RealmConfiguration _configuration;

        public static RealmConfiguration Configuration => _configuration ?? (_configuration = new RealmConfiguration());
    }
}
