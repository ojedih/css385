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
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        CmdSendInput();
        healthText.text = $"{state.hp}";
    }

    void FixedUpdate()
    {
        if (!isServer) return;
        
        movement.Move(serverMoveInput);

        if(state.defusing)
            nuke.TryDefuse(this);
        else
            nuke.StopDefuse();
    }

    void LateUpdate()
    {
        if (!isLocalPlayer || defuseSlider == null) return;
        defuseSlider.gameObject.SetActive(nuke.defuseProgress > 0);
        defuseSlider.value = nuke.defuseProgress / nuke.defuseTime;
    }

    [Command]
    void CmdSendInput()
    {
        serverMoveInput = input.moveInput.normalized;
        state.aimDirection = input.aimDirection;
        state.defusing = input.defusePressed;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        nuke = GameObject.FindWithTag("Nuke").GetComponent<Nuke>();

        // Count players already in the game
        int connectedPlayers = NetworkServer.connections.Count;

        // First 3 players → team 0, next 3 → team 1
        team = (connectedPlayers <= 3) ? 0 : 1;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.SetTarget(transform);

        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();
        
        Canvas canvas = FindFirstObjectByType<Canvas>();
        defuseSlider = Instantiate(defuseSliderPrefab, canvas.transform);
    }
}
