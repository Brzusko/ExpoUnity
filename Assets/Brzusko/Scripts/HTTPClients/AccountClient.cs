using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brzusko.HTTP
{
    public class AccountClient : BaseClient
    {
        protected override string BaseURI { get => _config.AccountURL; }

        protected readonly string _createPath = "";

        public AccountClient(BackendConfigSO config) : base(config)
        {
        } 
    }
}

