using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item: MonoBehaviour
{
    /*public Vector3 pickUpPosition;
    public Vector3 pickUpRotation;

    public void PickUp(Interactor interactor)
    {
        PhotonNetwork.Destroy(gameObject);
    }*/
    private Transform _originalParent;
    private Rigidbody _rb;

    void Start()
    {
        _originalParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform handTransform)
    {
        _rb.isKinematic = true;
        transform.SetParent(handTransform);
        transform.position = handTransform.position;
    }

    public void Drop()
    {
        _rb.isKinematic = false;
        transform.SetParent(null);
    }
}
