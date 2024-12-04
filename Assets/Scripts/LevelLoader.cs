using UnityEngine;
using UnityEngine.UIElements;

//[ExecuteAlways]
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private Transform grid;
    //Game Manager

    [SerializeField] private float xGridFactor;
    [SerializeField] private float yGridFactor;

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        foreach (var elem in levelData.cells)
        {
            float xPos = elem.position.x * xGridFactor;
            float yPos = elem.position.y * yGridFactor;

            Vector3 spawnPoint = new Vector3 (xPos, 0, yPos);
            var spawnedElement = Instantiate(elem.element, spawnPoint, Quaternion.identity);
            spawnedElement.transform.SetParent(grid);
            GameManager.Instance.MaxMoves = levelData.MaxMoves;
            GameManager.Instance.MovesLeft = levelData.MaxMoves;
        }
    }
}
