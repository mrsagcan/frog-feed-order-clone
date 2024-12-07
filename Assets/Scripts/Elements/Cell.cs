using UnityEngine;

public class Cell : Element
{
    [HideInInspector] public int xPos, yPos, zPos;

    private void OnDisable()
    {
        Actions.OnCellDisable(this);
    }

    public void SetGridPos(Vector3 GridPos)
    {
        xPos = (int)GridPos.x;
        yPos = (int)GridPos.y;
        zPos = (int)GridPos.z;
    }
}
