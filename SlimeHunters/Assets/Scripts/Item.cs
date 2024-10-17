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
    private PhotonView photonView;

    void Start()
    {
        _originalParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    public void PickUp(Transform handTransform)
    {
        photonView.RPC("RPC_PickUp", RpcTarget.All, handTransform.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    public void RPC_PickUp(int handTransformViewID)
    {
        Transform handTransform = PhotonView.Find(handTransformViewID).transform;
        _rb.isKinematic = true;
        transform.SetParent(handTransform);
        transform.position = handTransform.position;
    }

    public void Drop()
    {
        photonView.RPC("RPC_Drop", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_Drop()
    {
        _rb.isKinematic = false;
        transform.SetParent(_originalParent);
    }
}
