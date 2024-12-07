using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : Element, ICollectable
{
    [SerializeField] private float _collectSpeed = 10f;
    private List<Vector3> _route;
    private Animator _animator;
    private bool _collecting;
    private Vector3 _currentTarget;

    [HideInInspector] public bool HasCollected { get ; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _route = new List<Vector3>();
        HasCollected = false;
    }

    private void Update()
    {
        if(_collecting)
        {
            MoveWithTheRoute();
        }
    }

    public void Found()
    {
        _animator.SetTrigger("Found");
    }

    public void Collect(List<Vector3> route)
    {
       _route.AddRange(route);
        _collecting = true;
        _currentTarget = _route[_route.Count - 1];
    }
    private void MoveWithTheRoute()
    {
        //If it reached its final destination.
        if(transform.position == _route[0])
        {
            _collecting = false;
            HasCollected = true;
            transform.parent.gameObject.SetActive(false);
            return;
        }

        //If the current position is not yet equal to the current target
        if(transform.position != _currentTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _collectSpeed * Time.deltaTime);
        }
        //If it is, then set the next target.
        else
        {
            _currentTarget = _route[_route.Count - 2];
            _route.RemoveAt(_route.Count - 1);
        }
    }
}
