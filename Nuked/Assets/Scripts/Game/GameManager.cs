using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;


public class GameManager : NetworkBehaviour
{
    [SyncVar] public float timeToDeath = 120f;
    [SyncVar] public int blueTeamScore = 0;
    [SyncVar] public int redTeamScore = 0;

    public TMP_Text timerLabel;
    public AudioSource tension_buildup;
    public VideoPlayer videoPlayer;
    Nuke nuke;

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            timeToDeath -= Time.deltaTime;
            if (timeToDeath < 0)
            {
                timeToDeath = 0;
                RpcDetonateNuke();
            }

            if (timeToDeath < 60f && !tension_buildup.isPlaying)
                tension_buildup.Play();
        }

        UpdateTime();
    }

    void UpdateTime()
    {
        int seconds = Mathf.FloorToInt(timeToDeath % 60);
        int minutes = Mathf.FloorToInt(timeToDeath / 60);
        timerLabel.text = $"{minutes:00}:{seconds:00}";
    }

    public override void OnStartServer()
    {
        nuke = GameObject.FindWithTag("Nuke").GetComponent<Nuke>();
    }

    [ClientRpc]
    public void RpcNukeDefused(int team)
    {
        if(team == 0)
            blueTeamScore += 1;
        else
            redTeamScore += 1;
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
