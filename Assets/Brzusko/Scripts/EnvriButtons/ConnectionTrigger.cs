using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTrigger : MonoBehaviour
{
    private async Task EnterSimulation()
    {
        await Bootstrapper.Instance.LoadScene(SceneType.Simulation);
    }

    private async void OnTriggerEnter(Collider other)
    {
        await EnterSimulation();
    }
}
