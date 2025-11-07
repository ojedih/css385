/*
PlayerState.cs (Server + Client)

Holds the truth: position, rotation, HP, etc.
*/

using Mirror;
using UnityEngine;

public class PlayerState : NetworkBehaviour
{
    [SyncVar] public Vector2 position;
    [SyncVar] public Vector2 aimDirection;
}