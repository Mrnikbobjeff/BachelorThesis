using System;

namespace BPTest.Shared.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
