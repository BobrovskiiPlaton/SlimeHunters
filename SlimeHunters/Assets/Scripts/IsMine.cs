using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IsMine : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _camera;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            _playerController.enabled = false;
            _camera.SetActive(false);
        }
    }
}
