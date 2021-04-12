using BPTest.Services.Interfaces;
using BPTest.Shared.Constants;
using BPTest.Shared.Exceptions;
using BPTest.Shared.Models.Authentication;
using NLog;
using NodaTime.Extensions;
using Polly;
using SensorHub.Backend;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BPTest.Services
{
    public class LoginService : ILoginService
    {
        readonly ISwaggerClient client;
        readonly ISettingsService settingsService;
        readonly Policy loginPolicy = Policy.Handle<HttpRequestException>()
            .Retry(3);
        readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public LoginService(ISwaggerClient client, ISettingsService settingsService)
        {
            this.client = client;
            this.settingsService = settingsService;
        }

        public async Task<bool> IsLoggedIn()
        {
            var token = await settingsService.Get(SettingsKeys.AuthorizationTokenSettingsKey, default(AuthorizationToken));
            if (!(token is null))
                return true;
            return false;
        }

        public async Task<bool> Login(string email, string password)
        {
            var credentials = new Credentials { Client = "BachelorProjekt Niklas Schilli", Email = email, Password = password };
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    var loginResponse = await loginPolicy.Execute(() => client.LoginAsync(credentials, cts.Token));
                    var authorizationToken = new AuthorizationToken(loginResponse.Token, loginResponse.RefreshToken);
                    await settingsService.Save(SettingsKeys.AuthorizationTokenSettingsKey, authorizationToken).ConfigureAwait(false);
                    client.UpdateAuthToken(authorizationToken.Token);
                    //await settingsService.Save(SettingsKeys.UserAccountNameKey, authorizationToken).ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new LoginException("Failed to login", ex);
            }
        }

        public async Task Logout()
        {
            var token = await settingsService.Get(SettingsKeys.AuthorizationTokenSettingsKey, default(AuthorizationToken));
            if (token is null)
                return;
            await loginPolicy.Execute(() => client.RevokeRefreshTokenAsync(token.RefreshToken)).ConfigureAwait(false);
            await settingsService.Delete(SettingsKeys.AuthorizationTokenSettingsKey).ConfigureAwait(false);
        }
    }
}
