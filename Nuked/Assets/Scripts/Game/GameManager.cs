using Mirror;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour
{

    [SyncVar] public float timeToDeath = 120f;
    public TMP_Text timerLabel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            timeToDeath -= Time.deltaTime;
            if (timeToDeath < 0) timeToDeath = 0;
        }

        int seconds = Mathf.FloorToInt(timeToDeath % 60);
        int minutes = Mathf.FloorToInt(timeToDeath / 60);
        timerLabel.text = $"{minutes:00}:{seconds:00}";
    }
    
    
}
