using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager: MonoBehaviour
{
    public InputField moveForwardInputField;
    public InputField moveBackwardInputField;
    public InputField moveLeftInputField;
    public InputField moveRightInputField;
    public InputField pickUpInputField;
    public InputField jumpInputField;
    
    //public InputField sprintInputField;
    //public InputField crouchInputField;
    private ControlManager controlManager;

    void Start()
    {
        controlManager = FindObjectOfType<ControlManager>();
        LoadSettings();
    }

    public void UpdateControlSettings()
    {
        controlManager.controlSettings.moveForward = moveForwardInputField.text;
        controlManager.controlSettings.moveBackward = moveBackwardInputField.text;
        controlManager.controlSettings.moveLeft = moveLeftInputField.text;
        controlManager.controlSettings.moveRight = moveRightInputField.text;
        controlManager.controlSettings.pickUp = pickUpInputField.text;
        controlManager.controlSettings.jump = jumpInputField.text;
        
        //controlManager.controlSettings.sprint = sprintInputField.text;
        //controlManager.controlSettings.crouch = crouchInputField.text;

        controlManager.SaveSettings();
    }

    private void LoadSettings()
    {
        jumpInputField.text = controlManager.controlSettings.jump;
    }
}