using UnityEngine;
using UnityEngine.UIElements;

//[ExecuteAlways]
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private Transform grid;

    [SerializeField] private float xGridFactor;
    [SerializeField] private float yGridFactor;


    public void LoadLevel()
    {
        NodeManager.Instance.UpdateSize(levelData.rows, levelData.columns);

        //For each element in the level data, spawn it or deactivate it.
        foreach (var elem in levelData.cells)
        {
            //Find the element's world position.
            float xPos = elem.position.x * xGridFactor;
            float yPos = elem.position.y * yGridFactor;
            Vector3 spawnPoint = new Vector3 (xPos, 0, yPos);

            var spawnedElement = Instantiate(elem.element, spawnPoint, Quaternion.identity);
            spawnedElement.SetGridPos(elem.position);
            //Deactivate it, if it is less than zero. Meaning there are other cells above it in this node.
            if(elem.position.z < 0)
            {
                spawnedElement.gameObject.SetActive(false);
            }
            spawnedElement.transform.SetParent(grid);

            //Add it to the node manager and update game manager values.
            NodeManager.Instance.AddCell(spawnedElement);
            GameManager.Instance.MaxMoves = levelData.MaxMoves;
            GameManager.Instance.MovesLeft = levelData.MaxMoves;
            if(spawnedElement.GetComponentInChildren<Frog>() != null)
            {
                GameManager.Instance.ActiveFrogsCount++;
            }
        }
    }
}
