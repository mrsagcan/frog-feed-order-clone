using System.Collections.Generic;
using UnityEngine;

public class Arrow : Element, ICollectable
{
    public Direction pose;

    [HideInInspector] public bool HasCollected { get ; set ; }

    private void Awake()
    {
        HasCollected = false;
    }

    public void Collect(List<Vector3> route)
    {
        transform.parent.gameObject.SetActive(false);
        HasCollected = true;
    }
}
