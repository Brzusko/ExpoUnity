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
        protected bool _isActionDone = true;
        public BaseClient()
        {
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

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(dataAsJson);

            var result = new BackendResult<T>();
            // fix https://stackoverflow.com/questions/60862424/how-to-post-my-data-using-unitywebrequest-post-api-call
            // propably jsonUtility is corrupted
            using var request = new UnityWebRequest(uri, "POST");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            foreach(var header in _headers)
                request.SetRequestHeader(header.Item1, header.Item2);

            Debug.Log("Sending poest request to " + uri);
            
            try
            {
                var operation = request.SendWebRequest();

                while(!operation.isDone)
                    await Task.Yield();
                
                _isActionDone = true;
                
                if(request.result != UnityWebRequest.Result.Success)
                {
                    result.Error = new BackendError{ errorMessage = request.error, errorCode = (int)request.responseCode };
                    Debug.LogWarning("Error with request: " + uri + " ### " + result.Error.errorMessage);
                    return result;
                }

                var responseData = request.downloadHandler.text;

                if(request.responseCode == _errorStatusCode)
                {
                    var error = JsonUtility.FromJson<BackendError>(responseData);
                    result.Error = error;
                    Debug.LogWarning(error.errorMessage);
                    Debug.LogWarning("Error with query " + uri);
                    return result;
                }

                Debug.LogWarning(responseData);
                var data = JsonUtility.FromJson<T>(responseData);
                result.Result = data;

            }
            catch(Exception ex)
            {
                Debug.LogWarning("Parsing JSON error: " + ex.Message);
                return null;
            }

            return result;
        }
    }
}

