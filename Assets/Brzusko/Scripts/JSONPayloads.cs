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
    public class LoginAuth : Payload
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
        public string accessToken;
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
        public string message;
        public Credentials credentials;
    }
}
