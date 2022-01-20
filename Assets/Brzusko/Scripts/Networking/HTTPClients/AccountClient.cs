using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brzusko.JSONPayload;
namespace Brzusko.HTTP
{
    public class AccountClient : BaseClient
    {
        protected override string BaseURI { get => BackendStaticConfig.AccountURL; }

        protected readonly string _createPath = "/create";
        protected readonly string _getPath = "/get";

        public AccountClient()
        {
        }

        public async Task<RegisterSuccess> CreateAccount(string name)
        {
            var registerData = new Register { name = name };
            
            RegisterSuccess response = null;

            var result = await this.Post<RegisterSuccess, Register>(_createPath, registerData);
            if(result.Error != null)
                return response;
            
            response = result.Result;

            return response;
        }

        public async Task<AccountDetails> GetAccountInfo(string token)
        {
            var accessToken = new AccessToken { token = token };
            AccountDetails response = null;
            var result = await this.Post<AccountDetails, AccessToken>(_getPath, accessToken);

            if(result.Error != null)
                return response;
            
            response = result.Result;
            return response;
        }
    }
}

