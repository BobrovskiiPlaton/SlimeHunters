using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item: MonoBehaviour
{
    public Vector3 pickUpPosition;
    public Vector3 pickUpRotation;

    public void PickUp(Interactor interactor)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
