
namespace Brzusko.JSONPayload.Request
{
    public class Register
    {
        public string name;
    }

    public class LoginCred
    {
        public string name;
        public int pinCode;
    }

    public class LoginAuth
    {
        public string token;
    }
}

namespace Brzusko.JSONPayload.Response
{
    public class BackendError
    {
        public int errorCode;
        public string errorMessage;
    }

    public class Credentials
    {
        public string accessToken;
        public string refreshToken;
    }

    public class Success
    {
        public string message;
    }

    public class AuthSuccess
    {
        public string message;
        public Credentials credentials;
    }
}