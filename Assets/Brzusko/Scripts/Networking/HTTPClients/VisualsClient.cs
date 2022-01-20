using System;
using System.Threading.Tasks;
using Brzusko.JSONPayload;
using UnityEngine;

namespace Brzusko.HTTP
{
    public class VisualsClient : BaseClient
    {
        protected override string BaseURI { get => BackendStaticConfig.VisualsURL; }
        protected static readonly string _getSexPath = "/get";
        protected static readonly string _updateSex = "/update";
        public async Task<string> GetSex(string token)
        {
            var accessToken = new AccessToken{ token = token };
            var result = await this.Post<SexSuccess, AccessToken>(_getSexPath, accessToken);
            return result.Result.sex;
        }

        public async Task<string> UpdateSex(string token, int sex)
        {        
            var updatePayload= new SexUpdate { token = token, sex = sex };
            var result = await this.Post<SexSuccess, SexUpdate>(_updateSex, updatePayload);
            return result.Result.sex;
        }
    }
}