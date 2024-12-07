using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public int MaxMoves;
    [HideInInspector] public int MovesLeft;
    public int ActiveFrogsCount;

    [SerializeField] private Transform grid;
    private LevelLoader _currentLevelLoader;

    public enum GameState
    {
        Start, InGame, Finish, GameOver 
    }

    public GameState State;

    private void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        State = GameState.Start;
    }

    //Find the level loader of this scene and load the level.
    public void StartGame()
    {
        _currentLevelLoader = FindObjectOfType<LevelLoader>();
        _currentLevelLoader.LoadLevel();
        State = GameState.InGame;
    }

    public void FinishGame()
    {
        State = GameState.Finish;
        Actions.OnLevelSuccesful();
        SceneLoader.Instance.LoadNextLevel();
    }

    public void GameOver()
    {
        State = GameState.GameOver;
        Actions.OnLevelFailed();
        SceneLoader.Instance.RestartLevel();
    }

    private void OnEnable()
    {
        Actions.OnFrogClicked += FrogClicked;
        Actions.OnTongueCollect += OnTongueCollect;
    }

    private void OnDisable()
    {
        Actions.OnFrogClicked -= FrogClicked;
        Actions.OnTongueCollect -= OnTongueCollect;
    }

    private void FrogClicked()
    {
        MovesLeft--;
        Actions.OnMovesLeftChanged(MovesLeft);
        //If player is out of moves and there are still frogs out there.
        if(MovesLeft == 0 && ActiveFrogsCount > 0)
        {
            GameOver();
        }
    }

    private void OnTongueCollect()
    {
        ActiveFrogsCount--;
        if(ActiveFrogsCount == 0)
        {
            //Check if there are still active berries. If not then player won.
            if(CheckOthers())
            {
                FinishGame();
            }
            //If there active ones, then player lost.
            else
            {
                GameOver();
            }
        }
    }

    private bool CheckOthers()
    {
        foreach(Transform cell in grid)
        {
            if(cell.GetComponent<Cell>() != null && cell.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
