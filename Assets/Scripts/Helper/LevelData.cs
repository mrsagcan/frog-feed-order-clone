using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    //TODO: For now the game is only 5x5, if i have time I will make it so that the background and camera is adjusted to different grid sizes.
    [HideInInspector] public int rows = 5;
    [HideInInspector] public int columns = 5;
    [SerializeField] public CellDictionary[] cells;
    public int MaxMoves = 20;
}



[Serializable]
public struct CellDictionary
{
    [SerializeField] public Vector3 position;
    [SerializeField] public Element element;
}
