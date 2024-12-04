using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [HideInInspector] public int MaxMoves;
    public int MovesLeft;

    private void OnEnable()
    {
        Actions.OnFrogClicked += FrogClicked;
    }

    protected override void OnDisable()
    {
        Actions.OnFrogClicked -= FrogClicked;
        base.OnDisable();
    }

    private void FrogClicked()
    {
        MovesLeft--;
    }
}
