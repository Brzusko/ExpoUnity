using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private AssetReference[] _crucialScenes;
    
    private async void Start()
    {
        await LoadBasicScenes();
    }

    private void ConnectEvents()
    {

    }

    private void DisconnectEvents()
    {

    }
    private async Task<SceneInstance[]> LoadBasicScenes()
    {
        var loadingScenes = Enumerable.Range(0, _crucialScenes.Length).Select(_ => _crucialScenes[_].LoadSceneAsync(LoadSceneMode.Additive).Task);
        var result = await Task.WhenAll(loadingScenes);
        return result;
    }
}
