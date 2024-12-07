using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager : Singleton<NodeManager>
{
    public List<List<List<Cell>>> Grid;

    private void OnEnable()
    {
        Actions.OnCellDisable += ActivateNextCell;
    }

    private void OnDisable()
    {
        Actions.OnCellDisable -= ActivateNextCell;
    }

    //Updates the size of the 3D list with given numbers.
    public void UpdateSize(int rows, int columns)
    {
        Grid = new List<List<List<Cell>>>();
        for(int i = 0; i < rows; i++)
        {
            Grid.Add(new List<List<Cell>>());
            for(int j = 0; j < columns; j++)
            {
                Grid[i].Add(new List<Cell>());
            }
        }
    }

    public void AddCell(Cell cell)
    {
        Grid[cell.xPos][cell.yPos].Add(cell);
    }

    //Activate the cell that is below this object.
    public void ActivateNextCell(Cell cell)
    {
        var NextCell = Grid[cell.xPos][cell.yPos].Find(c => cell.zPos-1 == c.zPos);
        if(NextCell != null)
        {
            NextCell.gameObject.SetActive(true);
        }
    }
}
