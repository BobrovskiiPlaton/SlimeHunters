using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float _speed;
    public Transform player;
    public Camera camera;
    private PhotonView view;
    
    
    //Для камеры
    private Quaternion StartingRotation;
    private float inputX;
    private float inputY;
    public float sensitivity;

    // Update is called once per frame
    void Start()
    {
        StartingRotation = transform.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponentInChildren<Camera>();
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {
        if (view.IsMine)
        {
            Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Jump"));
            inputDir.Normalize();
            Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x + transform.up * inputDir.z) * _speed;
            transform.position += velocity * Time.deltaTime;
            
            inputX += Input.GetAxis("Mouse X") * sensitivity;
            inputY += Input.GetAxis("Mouse Y") * sensitivity;
            inputY = Mathf.Clamp(inputY, -90f, 90f);
            Quaternion RotX = Quaternion.AngleAxis(inputX, Vector3.up);
            Quaternion RotY = Quaternion.AngleAxis(-inputY, Vector3.right);
            player.transform.rotation = StartingRotation * RotX;
            camera.transform.rotation = StartingRotation * RotX * RotY;
        }
        
    }

}