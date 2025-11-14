using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    PlayerInput input;
    PlayerState state;
    PlayerMovement movement;
    
    [SyncVar] public int team; // 0 = Team A, 1 = Team B

    Vector2 serverMoveInput;
    public TMP_Text healthText;

    public GameObject nuke;
    public float defuseTime = 10f;
    public float defuseDistance = 3f;
    [SyncVar] float defuseProgress = 0f;

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
        CmdSendInput(input.moveInput, input.aimDirection, input.shootPressed, input.defusePressed);
        healthText.text = $"{state.hp}";
    }

    void FixedUpdate()
    {
        if (!isServer) return;
        movement.Move(serverMoveInput);
    }

    void LateUpdate()
    {
        if (!isLocalPlayer || defuseSlider == null) return;
        defuseSlider.gameObject.SetActive(defuseProgress > 0);
        defuseSlider.value = defuseProgress / defuseTime;
    }

    [Command]
    void CmdSendInput(Vector2 move, Vector2 aim, bool shoot, bool defusePressed)
    {
        serverMoveInput = move.normalized;
        state.aimDirection = aim;

        float dist = Vector2.Distance(transform.position, nuke.transform.position);

        if (dist < defuseDistance && defusePressed)
        {
            Debug.Log("Bomb defusing");
            defuseProgress += Time.deltaTime;
            if (defuseProgress >= defuseTime)
            {
                defuseProgress = defuseTime;
                RpcBombDefused();
            }
        }
        else
        {
            // stop or left radius -> reset
            defuseProgress = 0f;
        }

    }

    [ClientRpc]
    void RpcBombDefused()
    {
        Debug.Log("Bomb Defused — Round Over");
        
        if (NetworkServer.active)
            NetworkManager.singleton.StopServer();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        nuke = GameObject.FindWithTag("Bomb");

        // Count players already in the game
        int connectedPlayers = NetworkServer.connections.Count;

        // First 3 players → team 0, next 3 → team 1
        team = (connectedPlayers <= 3) ? 0 : 1;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        nuke = GameObject.FindWithTag("Bomb");
        
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.SetTarget(transform);

        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();
        nuke = GameObject.Find("Nuke");
        
        Canvas canvas = FindFirstObjectByType<Canvas>();
        defuseSlider = Instantiate(defuseSliderPrefab, canvas.transform);
    }
}
