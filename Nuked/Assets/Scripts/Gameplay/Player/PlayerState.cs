/*
PlayerState.cs (Server + Client)

Holds the truth: position, rotation, HP, etc.
*/

using Mirror;
using UnityEngine;

public class PlayerState : NetworkBehaviour
{
    [SyncVar] public Vector2 aimDirection;
    [SyncVar] public int hp;
    [SyncVar] public bool defusing;

    public int maxHp = 100;

    [Server]
    public void TakeDamage(int damage)
    {
        if (!isServer) return;

        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            GetComponent<PlayerNetwork>().Die();
        }
    }

    [Server]
    public void ResetHealth()
    {
        hp = maxHp;
    }    
}