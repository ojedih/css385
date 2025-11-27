using Mirror;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SyncVar] public float timeToDeath = 120f;
    public TMP_Text timerLabel;
    public AudioSource tension_buildup;

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
                nuke.RpcDetonateNuke();
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
}
