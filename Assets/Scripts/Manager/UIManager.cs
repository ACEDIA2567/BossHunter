using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject optionUI;
    public GameObject dieUI;
    public GameObject stageUI;
    public TextMeshProUGUI stageTime;
    private TextMeshProUGUI bestClearTime;
    private float sec = 0;
    private int min = 0;

    private void Update()
    {
        StartStage();
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
        // bestClearTime.text = PlayerPrefs.GetFloat($"{clearStage}").ToString;

        // 현재 클리어 시간이 더 짧다면
        // 시간 단축 시 PlayerPrefs.SetFloat($"{clearStage}", float.Parse(stageTime.text));

        // 아니라면 갱신 X
    }

    public void StartStage()
    {
        sec += Time.deltaTime;
        if (sec >= 60f)
        {
            min += 1;
            sec = 0;
        }
        stageTime.text = $"{min:D2}:{(int)sec:D2}";
    }
}
