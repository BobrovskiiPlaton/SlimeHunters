using System.IO;
using UnityEngine;

public class ControlManager: MonoBehaviour
{
    public ControlSettings controlSettings;
    private string settingsFilePath;

    void Start()
    {
        settingsFilePath = Path.Combine(Application.persistentDataPath, "controlSettings.json");

        if (File.Exists(settingsFilePath))
        {
            LoadSettings();
        }
        else
        {
            controlSettings = new ControlSettings();
            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(controlSettings, true);
        File.WriteAllText(settingsFilePath, json);
    }

    public void LoadSettings()
    {
        string json = File.ReadAllText(settingsFilePath);
        controlSettings = JsonUtility.FromJson<ControlSettings>(json);
    }

    public void ChangeKey(string action, string newKey)
    {
        switch (action)
        {
            case "moveForward":
                controlSettings.moveForward = newKey;
                break;
            
            case "moveBackward":
                controlSettings.moveBackward = newKey;
                break;
            
            case "moveLeft":
                controlSettings.moveLeft = newKey;
                break;
            
            case "moveRight":
                controlSettings.moveRight = newKey;
                break;
            
            case "pickUp":
                controlSettings.pickUp = newKey;
                break;
            
            case "jump":
                controlSettings.jump = newKey;
                break;
            
            //case "sprint":
            //  controlSettings.sprint = newKey;
            //  break;
            
            //case "crouch":
            //  controlSettings.crouch = newKey;
            //  break;
        }
    }
}