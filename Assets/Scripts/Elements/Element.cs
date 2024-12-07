using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] public Color color;
}

public enum Color
{
    Blue, Green, Purple, Red, Yellow
}

public enum Direction
{
    Left, Right, Up, Down
}
