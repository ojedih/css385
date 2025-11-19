using Mirror;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class Nuke : NetworkBehaviour
{

    [SyncVar] public bool nukeDefused = false;
    [SyncVar] public float defuseProgress = 0f;
    
    public float defuseTime = 10f;
    public float defuseDistance = 3f;

    public VideoPlayer videoPlayer;

    // Update is called once per frame
    void Update()
    {

    }

    public void TryDefuse(PlayerNetwork player)
    {
        float playerDist = Vector2.Distance(player.transform.position, transform.position);

        if (playerDist < defuseDistance)
        {
            Debug.Log("Bomb defusing");
            defuseProgress += Time.deltaTime;
            if (defuseProgress >= defuseTime)
            {
                defuseProgress = defuseTime;
                RpcNukeDefused();
            }
        }
        else
        {
            // stop or left radius -> reset
            defuseProgress = 0f;
        }
    }

    public void StopDefuse()
    {
        defuseProgress = 0f;
    }

    [ClientRpc]
    void RpcNukeDefused()
    {
        Debug.Log("Nuke Defused — Round Over");
    }

    [ClientRpc]
    public void RpcDetonateNuke()
    {
        Debug.Log("Nuke Detonated — Round Over");
        videoPlayer.isLooping = false;
        videoPlayer.Play();
        StartCoroutine(WaitForVideoEnd());
    }

    IEnumerator WaitForVideoEnd()
    {
        yield return new WaitUntil(() => videoPlayer.frame >= (long)videoPlayer.frameCount - 1);
        if (isServer) NetworkManager.singleton.StopServer(); // or StopServer()
    }
}