using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro; // if using TextMeshPro

public class LobbyManager : NetworkBehaviour
{
    public Transform playerListContainer;
    public GameObject playerListEntryPrefab;
    public Button startGameButton;

    void Start()
    {
        RefreshPlayerList();
    }

    void Update()
    {
        // Host only gets Start button
        if (startGameButton != null)
            startGameButton.gameObject.SetActive(NetworkServer.active);
    }

    public void RefreshPlayerList()
    {
        // Clear old entries
        foreach (Transform child in playerListContainer)
            Destroy(child.gameObject);

        foreach (var conn in NetworkServer.connections)
        {
            string name = "Player " + conn.Key; // temporary name

            var entry = Instantiate(playerListEntryPrefab, playerListContainer);
            entry.GetComponentInChildren<TMP_Text>().text = name;
        }
    }

    public void StartMatch()
    {
        if (!NetworkServer.active) return; // Only host can start

        Debug.Log("Starting match...");
        NetworkManager.singleton.ServerChangeScene("Game");
    }
}
