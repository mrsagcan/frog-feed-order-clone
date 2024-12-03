using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] public int rows = 5;
    [SerializeField] public int columns = 5;
    [SerializeField] public CellDictionary[] cells;
}



[Serializable]
public struct CellDictionary
{
    [SerializeField] public Vector3 position;
    [SerializeField] public Element element;
}
