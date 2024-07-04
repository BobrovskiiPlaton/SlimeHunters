using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactableMask;
    private int numFound;
    

    private readonly Collider[] _colliders = new Collider[3];


    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            _interactableMask);

        if (numFound >= 1)
        {
            var item = _colliders[0].GetComponent<Item>();
            if (item != null && Input.GetKeyDown(KeyCode.E))
            {
                item.PickUp(this);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}