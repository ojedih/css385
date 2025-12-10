using Mirror;
using UnityEngine;

public class Nuke : NetworkBehaviour
{
    [SyncVar] public float defuseProgress = 0f;
    [SyncVar] public bool isBeingDefused = false;

    public float defuseTime = 10f;
    public float defuseDistance = 3f;

    public GameManager gameManager;

    private NetworkIdentity currentDefuser = null;

    public void TryDefuse(PlayerNetwork player)
    {
        if (isBeingDefused && currentDefuser != player.netIdentity)
            return; // Someone else is already defusing

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < defuseDistance)
        {
            if (!isBeingDefused)
            {
                isBeingDefused = true;
                currentDefuser = player.netIdentity;
            }

            defuseProgress += Time.unscaledDeltaTime;
            Debug.Log($"Defusing... {defuseProgress:F1}/{defuseTime}");

            if (defuseProgress >= defuseTime)
            {
                defuseProgress = defuseTime;
                gameManager.AddScore(player.team);
                gameManager.RpcNukeDefused();
                ResetDefuse();
            }
        }
        else
        {
            StopDefuse(player);
        }
    }

    public void StopDefuse(PlayerNetwork player = null)
    {
        if (player == null || currentDefuser == player.netIdentity)
        {
            ResetDefuse();
        }
    }

    private void ResetDefuse()
    {
        defuseProgress = 0f;
        isBeingDefused = false;
        currentDefuser = null;
    }
}