using System;
using System.Threading.Tasks;
using Brzusko.JSONPayload;

namespace Brzusko.HTTP
{
    public class VisualsClient : BaseClient
    {
        protected override string BaseURI { get => BackendStaticConfig.VisualsURL; }
        protected static readonly string _getSexPath = "/get";
        protected static readonly string _updateSex = "/update";
        public async Task<Sex> GetSex(string token)
        {
            var accessToken = new AccessToken{ token = token };
            var result = await this.Post<SexSuccess, AccessToken>(_getSexPath, accessToken);
            if(result.Error != null)
                return null;
            return result.Result.sex;
        }

        public async Task<Sex> UpdateSex(string token, int sex)
        {        
            var accessToken = new AccessToken { token = token };
            var result = await this.Post<SexSuccess, AccessToken>(_updateSex, accessToken);
            if(result.Error != null)
                return null;
            return result.Result.sex;
        }
    }
}