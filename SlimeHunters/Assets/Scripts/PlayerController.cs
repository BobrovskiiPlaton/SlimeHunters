using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using Photon.Pun;
using UnityEditor.IMGUI.Controls;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private const float DistanceToGround = 1.1f;
    private const float jumpCD = 0.5f;
    private Rigidbody _rb;
    private bool canJump = true;
    
    
    [SerializeField] public float _speed;
    [SerializeField] public float _jumpForce;
    public Camera camera;
    public Transform handTransform;

    //Для камеры
    private Quaternion StartingRotation;
    private float inputX;
    private float inputY;
    public float sensitivity;
    
    //Кнопки управления
    private ControlManager controlManager;
    
    //Точка взаимодействия
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactableMask;
    private int numFound;
    private Item carriedItem = null;
    private readonly Collider[] _colliders = new Collider[3];

    // Update is called once per frame
    void Start()
    {
        StartingRotation = camera.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        controlManager = FindObjectOfType<ControlManager>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        
    }

    void GetInput()
    {
        MovePlayer();
        MoveCamera();
        ItemInteraction();
        Jump();
    }
    private void Jump()
    {
        if (Input.GetKey(controlManager.controlSettings.jump) && IsGrounded() && canJump)
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
            canJump = false;
            StartCoroutine(JumpCooldownRoutine());
        }
    }

    private IEnumerator JumpCooldownRoutine()
    {
        yield return new WaitForSeconds(jumpCD);
        canJump = true;
    }

    private bool IsGrounded()
    {
        RaycastHit raycastHit;
        if (Physics.SphereCast(transform.position, 0.5f, -Vector3.up, out raycastHit, DistanceToGround))
            return true;
        return false;
    }
    private void MovePlayer()
    {
        Vector3 inputDir = Vector3.zero;

        if (Input.GetKey(controlManager.controlSettings.moveForward))
        {
            inputDir += Vector3.forward;
        }
        if (Input.GetKey(controlManager.controlSettings.moveBackward))
        {
            inputDir += Vector3.back;
        }
        if (Input.GetKey(controlManager.controlSettings.moveLeft))
        {
            inputDir += Vector3.left;
        }
        if (Input.GetKey(controlManager.controlSettings.moveRight))
        {
            inputDir += Vector3.right;
        }

        inputDir.Normalize();
    
        Vector3 velocity = (transform.forward * inputDir.z + transform.right * inputDir.x) * _speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void MoveCamera()
    {
        inputX += Input.GetAxis("Mouse X") * sensitivity;
        inputY += Input.GetAxis("Mouse Y") * sensitivity;
        inputY = Mathf.Clamp(inputY, -90f, 90f);
        Quaternion RotX = Quaternion.AngleAxis(inputX, Vector3.up);
        Quaternion RotY = Quaternion.AngleAxis(-inputY, Vector3.right);
        transform.rotation = StartingRotation * RotX;
        camera.transform.rotation = StartingRotation * RotX * RotY;
    }
    
    private void ItemInteraction()
    {
        if (Input.GetKeyDown(controlManager.controlSettings.pickUp))
        {
            Debug.Log(carriedItem);
            if (carriedItem == null)
            {
                PickItem();
            }
            else
            {
                DropItem();
            }
        }
    }
    private void PickItem()
    {
        numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            _interactableMask);
        
        if (numFound >= 1)
        {
            var item = _colliders[0].GetComponent<Item>();
            if (item != null)
            {
                carriedItem = item;
                item.PickUp(handTransform);
            }
        }
    }

    private void DropItem()
    {
        carriedItem.Drop();
        carriedItem = null;
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, -transform.up * DistanceToGround, Color.red);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}