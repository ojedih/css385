using Mirror;
using TMPro;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    PlayerInput input;
    PlayerState state;
    PlayerMovement movement;
    [SyncVar] public int team; // 0 = Team A, 1 = Team B

    Vector2 serverMoveInput;
    public TMP_Text healthText;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        state = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        CmdSendInput(input.moveInput, input.aimDirection, input.shootPressed);
        healthText.text = $"{state.hp}";

    }

    void FixedUpdate()
    {
        if (!isServer) return;
        movement.Move(serverMoveInput);
    }

    [Command]
    void CmdSendInput(Vector2 move, Vector2 aim, bool shoot)
    {
        serverMoveInput = move.normalized;
        state.aimDirection = aim;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

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
    }
}
