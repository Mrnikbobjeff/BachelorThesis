using BPTest.Services.Interfaces;
using BPTest.Shared.Exceptions;
using BPTest.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BPTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly ILoginService loginService;
        public LoginViewModel(ILoginService loginServices)
        {
            loginService = loginServices;
            Title = "Login";
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
        }
        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }

        string emailAdress;

        public string EmailAdress { get => emailAdress; set => SetProperty(ref emailAdress, value); }

        string password;

        public string Password { get => password; set => SetProperty(ref password, value); }

        bool isLoggedIn;

        public bool IsLoggedIn { get => isLoggedIn; set => SetProperty(ref isLoggedIn, value); }

        public override async Task OnAppearing()
        {
            IsLoggedIn = await loginService.IsLoggedIn();
        }

        async void OnLoginClicked(object obj)
        {
            try
            {
                if (await loginService.Login(EmailAdress, Password))
                    IsLoggedIn = true;
            }
            catch (LoginException ex)
            {
                await Shell.Current.DisplayAlert("Failed to login", ex.Message, "Ok");
            }
        }

        async void OnLogoutClicked(object obj)
        {
            await loginService.Logout();
            EmailAdress = string.Empty;
            Password = string.Empty;
        }
    }
}
