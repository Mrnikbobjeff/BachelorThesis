using NodaTime;
using SensorHub.Backend;

namespace BPTest.Shared.Models.Authentication
{
    public class AuthorizationToken
    {
        public AuthorizationToken(string token, RefreshToken refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; }
        public RefreshToken RefreshToken { get; }
    }
}
