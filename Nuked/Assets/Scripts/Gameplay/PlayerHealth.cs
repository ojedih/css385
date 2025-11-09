/*
PlayerHealth.cs (Server ONLY)

Handles all damage & death.
*/

using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar] public int hp = 100;

    public void ApplyDamage(int amount)
    {
        if (!isServer) return;

        hp -= amount;
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        // Temporary: just move to random location
        transform.position = Vector3.zero;
        hp = 100;
    }
}
