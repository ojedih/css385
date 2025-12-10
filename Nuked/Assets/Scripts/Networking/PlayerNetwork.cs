using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    PlayerInput input;
    PlayerState state;
    PlayerMovement movement;
    Nuke nuke;
    
    Vector2 serverMoveInput;
    [SyncVar] public int team; // 0 = Team Blue, 1 = Team Red

    TMP_Text healthText;

    public Slider defuseSliderPrefab;
    private Slider defuseSlider;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        state = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();

        //nuke = Nuke.Instance;
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        CmdSendInput(input.moveInput.normalized, input.aimDirection, input.defusePressed);
        healthText.text = $"{state.hp}";
    }

    void FixedUpdate()
    {
        if (!isServer) return;
        
        movement.Move(serverMoveInput);

        if(state.defusing)
            nuke.TryDefuse(this);
        else
            nuke.StopDefuse(this);
    }

    void LateUpdate()
    {
        if (!isLocalPlayer || defuseSlider == null) return;
        defuseSlider.gameObject.SetActive(nuke.defuseProgress > 0);
        defuseSlider.value = nuke.defuseProgress / nuke.defuseTime;
    }

    [Command]
    void CmdSendInput(Vector2 move, Vector2 aimDir, bool defusing)
    {
        serverMoveInput = move;
        state.aimDirection = aimDir;
        state.defusing = defusing;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        nuke = GameObject.Find("Nuke").GetComponent<Nuke>();

        // Count players already in the game
        int connectedPlayers = NetworkServer.connections.Count;

        // First 3 players → team 0, next 3 → team 1
        team = (connectedPlayers <= 3) ? 0 : 1;

        if(team == 0)
            transform.position = new Vector2(0f, -7.8f);
        else
            transform.position = new Vector2(0f, 29.5f);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.SetTarget(transform);

        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();

        nuke = GameObject.Find("Nuke").GetComponent<Nuke>();
        
        Canvas canvas = FindFirstObjectByType<Canvas>();
        defuseSlider = Instantiate(defuseSliderPrefab, canvas.transform);
    }
}
