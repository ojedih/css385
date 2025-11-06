using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class GameConfig {
    public int volume;
    public string nametag;
}

public class ConfigLoader : MonoBehaviour {
    [SerializeField] private Scrollbar volumeScrollbar;
    [SerializeField] private TMP_InputField nameTagInputField; 
    
    private GameConfig config;
    private string configPath;

    private float normalizedVolume;

    void Start() {
        configPath = Path.Combine(Application.persistentDataPath, "config.json");
        LoadConfig();
    }

    private void LoadConfig() {
        if (File.Exists(configPath)) {
            // Load the writable user config
            string json = File.ReadAllText(configPath);
            config = JsonUtility.FromJson<GameConfig>(json);
        } else {
            LoadCustomConfig();
        }

        ApplyValuesToUI();
    }

    private float GetNormalizedVolume() {
        return Mathf.Clamp01(config.volume / 100f);
    }

    public void LoadCustomConfig() {
        // Load default from Resources (read-only)
        TextAsset defaultJson = Resources.Load<TextAsset>("config");
        if (defaultJson == null) {
            Debug.LogError("Default config.json not found in Resources!");
            config = new GameConfig { volume = 100, nametag = "Player1" };
        } else {
            config = JsonUtility.FromJson<GameConfig>(defaultJson.text);
        }

        ApplyValuesToUI();
    }

    public void ApplyValuesToUI() {
        normalizedVolume = GetNormalizedVolume();
        AudioListener.volume = normalizedVolume;

        Debug.Log($"[ConfigLoader] Setting scrollbar to {normalizedVolume} (from {config.volume})");

        if (volumeScrollbar != null)
            volumeScrollbar.value = normalizedVolume;

        if (nameTagInputField != null)
            nameTagInputField.text = config.nametag;

        SaveConfig();
    }

    public void KeepSavedValues() {
        volumeScrollbar.value = GetNormalizedVolume();
        nameTagInputField.text = config.nametag;
    }

    public void SaveConfig() {
        if (volumeScrollbar != null)
            config.volume = Mathf.RoundToInt(volumeScrollbar.value * 100f);

        if (nameTagInputField != null)
            config.nametag = nameTagInputField.text;

        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(configPath, json);
    }
}
