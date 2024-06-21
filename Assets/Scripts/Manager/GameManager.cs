using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        Invoke("FightStart", 5);
    }

    public void IntroScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void InGameScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void FightStart()
    {
        UIManager.Instance.StartStage();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.Fight]);
        // 몬스터 생성
    }
}
