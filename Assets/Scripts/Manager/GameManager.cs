using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject monster;
    public Minotaur minotaur;
    public GameObject player;

    public void IntroScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.Intro]);
    }

    public void InGameScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.InGame]);
    }


    public void GameExit()
    {
        Application.Quit();
    }

    public void PlayerDie()
    {
        UIManager.Instance.PlayerDie();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.End]);
    }

    public void PlayerWin()
    {
        UIManager.Instance.StageClear();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.End]);
    }

    public void FightStart()
    {
        // ���� ����
        monster = GameObject.Find("Minotaur").gameObject;
        monster.SetActive(true);
        minotaur = monster.GetComponent<Minotaur>();
        // ī�޶�� ���� �����ֱ�
        CameraManager.Instance.SpawnCamera();
        // �� �� �ٽ� �÷��̾� ��ġ�� �̵�
        // �÷��̾�� ��ġ�ϸ� BGM ����
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.Fight]);
        // Ÿ�̸� ���� �� UI Ȱ��ȭ
        UIManager.Instance.StartStage();
    }
}
