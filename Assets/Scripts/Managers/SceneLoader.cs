using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{

    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex;
        //If this scene is the last one, don't load the start screen and load the first level.
        if(nextScene == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
