using UnityEngine;
using TMPro;
using Mirror;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public TMP_InputField ipInput;

    void Start()
    {
        // Ensure the main menu is visible at startup
        ShowMainMenu();
    }

    public void HostGame()
    {
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        NetworkManager.singleton.networkAddress = ipInput.text;
        NetworkManager.singleton.StartClient();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}