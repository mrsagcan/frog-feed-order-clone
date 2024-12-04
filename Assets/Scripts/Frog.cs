using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Element
{
    [SerializeField] private Direction pose;
    [SerializeField] private Tongue tongue;

    private void OnEnable()
    {
        Actions.OnFrogClicked += OnClicked;
    }

    private void OnDisable()
    {
        Actions.OnFrogClicked -= OnClicked;
    }

    public void OnClicked()
    {
        if(!tongue.isReleasing && !tongue.isRetracting)
        {
            tongue.Release();
        }
    }
}
