using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public void IntroScene()
    {
        SceneManager.LoadScene(0);
    }

    public void InGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GameExit()
    {
        Application.Quit();
    }

}
