using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject controlsPanel;

    void Start()
    {
        // Ensure the main menu is visible at startup
        ShowMainPanel();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape))
            ShowControls();
        if (Input.GetKeyUp(KeyCode.Escape))
            ShowMainPanel();
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
}
