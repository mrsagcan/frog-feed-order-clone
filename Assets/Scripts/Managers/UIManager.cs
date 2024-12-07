using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : NonDestroyableSingleton<UIManager>
{

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject EventSystem;
    [SerializeField] private TMP_Text movesLeftText;

    //Dont destroy UI.
    protected override void Awake()
    {
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(EventSystem);
        startScreen.SetActive(true);
        movesLeftText.gameObject.SetActive(false);
        base.Awake();
    }

    private void Start()
    {
        SceneLoader.Instance.LoadNextLevel();
    }

    private void OnEnable()
    {
        Actions.OnLevelSuccesful += ShowWinScreen;
        Actions.OnLevelFailed += ShowRestartScreen;
        Actions.OnMovesLeftChanged += ChangeMovesLeft;
    }

    private void OnDisable()
    {
        Actions.OnLevelSuccesful -= ShowWinScreen;
        Actions.OnLevelFailed -= ShowRestartScreen;
        Actions.OnMovesLeftChanged -= ChangeMovesLeft;
    }

    public void StartButton()
    {
        startScreen.SetActive(false);
        StartLevel();
    }

    private void ShowWinScreen()
    {
        movesLeftText.gameObject.SetActive(false);
        winScreen.SetActive(true);
    }

    private void ShowRestartScreen()
    {
        movesLeftText.gameObject.SetActive(false);
        restartScreen.SetActive(true);
    }

    public void RestartButton()
    {
        restartScreen.SetActive(false);
        StartLevel();
    }

    public void NextLevelButton()
    {
        winScreen.SetActive(false);
        StartLevel();
    }

    //Start level and get that level's UI values.
    private void StartLevel()
    {
        movesLeftText.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
        movesLeftText.text = "Moves Left: " + GameManager.Instance.MovesLeft;
    }

    private void ChangeMovesLeft(int movesLeft)
    {
        movesLeftText.text = "Moves Left: " + movesLeft;
    }
}
