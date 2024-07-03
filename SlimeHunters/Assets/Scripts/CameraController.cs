using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public GameObject cam;
    private Quaternion StartingRotation;
    private float inputX;
    private float inputY;
    public float sensitivity;
    


    void Start()
    {
        StartingRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    private void Update()
    {
            inputX += Input.GetAxis("Mouse X") * sensitivity;
            inputY += Input.GetAxis("Mouse Y") * sensitivity;
            inputY = Mathf.Clamp(inputY, -90f, 90f);
            Quaternion RotX = Quaternion.AngleAxis(inputX, Vector3.up);
            Quaternion RotY = Quaternion.AngleAxis(-inputY, Vector3.right);
            Player.rotation = StartingRotation * RotX;
            cam.transform.rotation = StartingRotation * RotX * RotY;
    }
}