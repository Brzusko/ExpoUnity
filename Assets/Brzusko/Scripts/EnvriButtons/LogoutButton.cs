using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutButton : BaseButton
{
    public override async void Interact()
    {
        base.Interact();
        var playerCredentials = PlayerCredentials.Instance;
        await playerCredentials.Logout();
    }
}
