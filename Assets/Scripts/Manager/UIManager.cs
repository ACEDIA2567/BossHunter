using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject optionUI;
    public GameObject dieUI;
    public GameObject stageUI;
    public TextMeshProUGUI stageTimer;
    public TextMeshProUGUI currentStageTime;
    public TextMeshProUGUI bestClearTime;
    private float sec = 0;
    private int min = 0;

    private void Update()
    {
        StartStage();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionActive();
        }
    }

    public void OptionActive()
    {
        if (optionUI.activeSelf)
        {
            optionUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            optionUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PlayerDie()
    {
        dieUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void StageClear(int clearStage)
    {
        stageUI.SetActive(true);

        // 클리어 시간 보이게
        // 최고 클리어 시간 보이게
        currentStageTime.text = $"{min:D2}:{(int)sec:D2}";
        bestClearTime.text = $"{PlayerPrefs.GetInt("minTime"):D2}:{(int)PlayerPrefs.GetFloat("secTime"):D2}";

        // 현재 클리어 min이 더 짧다면
        if (PlayerPrefs.GetInt("minTime") >= min)
        {
            // 현재 클리어 sec이 더 짧다면
            if (PlayerPrefs.GetFloat("secTime") > sec)
            {
                // 시간 저장
                PlayerPrefs.SetFloat("secTime", sec);
                PlayerPrefs.SetInt("minTime", min);
            }
        }
    }

    public void StartStage()
    {
        sec += Time.deltaTime;
        if (sec >= 60f)
        {
            min += 1;
            sec = 0;
        }
        currentStageTime.text = $"{min:D2}:{(int)sec:D2}";
    }
}
