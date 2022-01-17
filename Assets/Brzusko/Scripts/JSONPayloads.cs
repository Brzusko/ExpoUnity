using System;
namespace Brzusko.JSONPayload
{
    public abstract class Payload
    {

    }
    [Serializable]
    public class Register : Payload
    {
        public string name;
    }

    [Serializable]
    public class LoginCred : Payload
    {
        public string name;
        public int pinCode;
    }

    [Serializable]
    public class RefreshToken : Payload
    {
        public string token;
    }

    [Serializable]
    public class AccessToken : Payload
    {
        public string token;
    }

    [Serializable]
    public class BackendError : Payload
    {
        public int errorCode;
        public string errorMessage;
    }
    [Serializable]
    public class Credentials : Payload
    {
        public string authToken;
        public string refreshToken;
    }

    [Serializable]
    public class Success : Payload
    {
        public string message;
    }

    [Serializable]
    public class AuthSuccess : Payload
    {
        public Success message;
        public Credentials credentials;
    }

    [Serializable]
    public class RegisterSuccess : Payload
    {
        public Success success;
        public LoginCred credentials;
    }

    [Serializable]
    public class Sex : Payload
    {
        public string sex;
    }

    [Serializable]
    public class SexSuccess : Payload
    {
        public Success message;
        public Sex sex;
    }

    [Serializable]
    public class AccountDetails : Payload
    {
        public string name;
        public int power;
    }
}
