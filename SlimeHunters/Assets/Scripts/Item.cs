using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    /*public enum Type
    {
        Cube,
        NotCube
    }

    public Type itemType;*/

    public void PickUp(Interactor interactor)
    {
        Destroy(gameObject);
    }
}
