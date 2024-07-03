using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float _speed;
    public Transform player;

    private PhotonView view;

    // Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        }
        
    }

}