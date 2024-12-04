using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private Color color;
}

public enum Color
{
    Blue, Green, Purple, Red, Yellow
}

public enum Direction
{
    Left, Right, Up, Down
}
