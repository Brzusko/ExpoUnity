using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct AuthRequest : NetworkMessage
{
    public enum Status : byte
    {
        NONE,
        OK,
        REJECT,
    }
    public string AccessToken;
    public Status ResponseStatus;
}
