using System;
using System.Threading.Tasks;
using Brzusko.JSONPayload;
using UnityEngine;
namespace Brzusko.HTTP
{
    public class LoginClient : BaseClient
    {
        protected override string BaseURI { get => BackendStaticConfig.AuthURL; }
        public LoginClient() {}
        protected static readonly string _loginCredPath = "/loginCred";
        protected static readonly string _loginRefPath = "/loginRef";
        protected static readonly string _logoutPath = "/logout";

        public async Task<Credentials> LoginCred(string name, int pinCode)
        {
            var loginCred = new LoginCred{ name = name, pinCode = pinCode};
            var result = await this.Post<AuthSuccess, LoginCred>(_loginCredPath, loginCred);
            Credentials credentials = null;

            if(result.Result == null)
            {
                return credentials;
            }

            credentials = result.Result.credentials;
            return credentials;
        }

        public async Task<Credentials> LoginRef(string token)
        {
            var loginToken = new RefreshToken{ token = token };
            var result = await this.Post<AuthSuccess, RefreshToken>(_loginRefPath, loginToken);
            
            Credentials credentials = null;

            if(result.Result == null)
            {
                return credentials;
            }

            credentials = result.Result.credentials;
            return credentials;
        }

        public async Task<Success> Logout(string token)
        {
            var loginToken = new RefreshToken{ token = token };
            var result = await this.Post<Success, RefreshToken>(_logoutPath, loginToken);

            Success success = null;

            if(result.Result == null)
            {
                return null;
            }
            
            success = result.Result;
            return success;
        }
    }
}