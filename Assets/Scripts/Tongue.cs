using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private BoxCollider boxCollider;

    [HideInInspector] public bool isReleasing = false;
    [HideInInspector] public bool isRetracting = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();        
    }

    public void Release()
    {
        isReleasing = true;
        Initialize();
    }

    private void Initialize()
    {

    }
}
