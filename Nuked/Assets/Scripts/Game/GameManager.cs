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
    public TMP_Text blueScoreText;
    public TMP_Text redScoreText;
    public AudioSource tension_buildup;
    public VideoPlayer videoPlayer;
    public GameObject blueWinScreen;
    public GameObject redWinScreen;
    public GameObject gamePanel;

    // Update is called once per frame
    
    void Start()
    {
        blueWinScreen.SetActive(false);
        redWinScreen.SetActive(false);
    }
    
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

    void UpdateScore(int team)
    {
        if(isServer)
        {
            if(team == 0)
                blueTeamScore += 1;
            else
                redTeamScore += 1; 
        }

        blueScoreText.text = $"{blueTeamScore}";
        redScoreText.text = $"{redTeamScore}";
    }

    [ClientRpc]
    public void RpcNukeDefused(int team)
    {
        UpdateScore(team);
        
        if(blueTeamScore == 5)
            TeamWin(0);
        else if(redTeamScore == 5)
            TeamWin(1);
        
        NextRound();
    }

    [ClientRpc]
    public void RpcDetonateNuke()
    {
        Debug.Log("Nuke Detonated — Round Over");
        videoPlayer.isLooping = false;
        videoPlayer.Play();
        StartCoroutine(WaitForVideoEnd());
    }

    [ClientRpc]
    public void TeamWin(int team)
    {
        gamePanel.SetActive(false);

        if(team == 0)
            blueWinScreen.SetActive(true);
        else
            redWinScreen.SetActive(true);

        ScheduleServerShutdown(10f);
    }

    [Server]
    public void NextRound()
    {
        timeToDeath = 120f;
        tension_buildup.Stop();

        foreach (var conn in NetworkServer.connections.Values)
        {
            if (conn.identity == null) continue;
            var player = conn.identity.GetComponent<PlayerNetwork>();
            if (player != null)
                player.RoundReset();
        }
    }

    [Server]
    public void ScheduleServerShutdown(float delay = 10f)
    {
        StartCoroutine(ShutdownAfterDelay(delay));
    }

    private IEnumerator ShutdownAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);   // waits 10 seconds (real time)

        // Stop everything properly
        if (NetworkServer.active)
            NetworkManager.singleton.StopServer();   // stops server + host
    }

    IEnumerator WaitForVideoEnd()
    {
        yield return new WaitUntil(() => videoPlayer.frame >= (long)videoPlayer.frameCount - 1);
        if (isServer) NetworkManager.singleton.StopServer();
    }
}
