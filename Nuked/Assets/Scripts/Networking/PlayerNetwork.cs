using Mirror;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    PlayerInput input;
    PlayerState state;
    PlayerMovement movement;
    [SyncVar] public int team; // 0 = Team A, 1 = Team B

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        state = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            // Local input controls only what we send to server
            CmdSendInput(input.moveInput, input.aimDirection, input.shootPressed);
        }
    }

    [Command]
    void CmdSendInput(Vector2 move, Vector2 aim, bool shoot)
    {
        // Server updates state
        state.aimDirection = aim;
        movement.Move(state, move);

        // Shooting will happen here soon
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Count players already in the game
        int connectedPlayers = NetworkServer.connections.Count;

        // First 3 players → team 0, next 3 → team 1
        team = (connectedPlayers <= 3) ? 0 : 1;
    }
}
