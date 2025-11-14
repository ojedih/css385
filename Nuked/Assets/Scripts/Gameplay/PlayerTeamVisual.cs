using Mirror;
using UnityEngine;

public class PlayerTeamVisual : NetworkBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        // Color based on team
        if (spriteRenderer != null)
        {
            spriteRenderer.color = (GetComponent<PlayerNetwork>().team == 0)
                ? Color.blue
                : Color.red;
        }
    }
}
