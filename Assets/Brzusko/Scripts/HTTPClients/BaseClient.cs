using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Brzusko.JSONPayload;

namespace Brzusko.HTTP
{
    public class BaseClient 
    {
        protected BackendConfigSO _config;
        protected bool _isActionDone = true;
        public BaseClient(BackendConfigSO config)
        {
            _config = config;
        }
        protected virtual string BaseURI { get; set; }
        protected readonly long _errorStatusCode = 201;
        protected List<Tuple<String, String>> _headers = new List<Tuple<string, string>> {
            new Tuple<string, string>("Content-Type", "application/json")
        };
        
        public virtual async Task<BackendResult<T>> Post<T, D>(string relativePath, D dataToSend) 
        where T : Payload
        where D : Payload
        {
            if(!_isActionDone) return null;
            _isActionDone = false;
            var uri = BaseURI + relativePath;
            var dataAsJson = JsonUtility.ToJson(dataToSend);
            Debug.Log("Serialized data: " + dataAsJson);

            var result = new BackendResult<T>();

            using var request = UnityWebRequest.Post(uri, dataAsJson);

            foreach(var header in _headers)
                request.SetRequestHeader(header.Item1, header.Item2);

            Debug.Log("Sending poest request to " + uri);

            var operation = request.SendWebRequest();

            while(!operation.isDone)
                await Task.Yield();
            
            _isActionDone = true;
            
            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error with request: " + uri);
                result.Error = new BackendError{ errorMessage = request.error, errorCode = (int)request.responseCode };
                return result;
            }

            var responseData = request.downloadHandler.text;

            try
            {
                if(request.responseCode == _errorStatusCode)
                {
                    var error = JsonUtility.FromJson<BackendError>(responseData);
                    result.Error = error;
                    Debug.LogError("Error with query " + uri);
                    return result;
                }

                var data = JsonUtility.FromJson<T>(responseData);
                result.Result = data;

            }
            catch(Exception ex)
            {
                Debug.LogError("Parsing JSON error: " + ex.Message);
                return null;
            }


            return result;
        }
    }
}

