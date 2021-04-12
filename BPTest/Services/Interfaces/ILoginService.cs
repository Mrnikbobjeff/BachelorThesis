using System.Threading.Tasks;

namespace BPTest.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> Login(string email, string password);
        Task Logout();
        Task<bool> IsLoggedIn();
    }
}
