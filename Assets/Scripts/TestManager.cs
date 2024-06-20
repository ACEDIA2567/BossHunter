using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    [SerializeField] private Text PlayerHpText;
    [SerializeField] private Player player;

    private float damagePeriod = 5f;
    private float damage = 100f;
    private float currentTime = 0f;

    void Start()
    {
        SetPlayerHPText();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > damagePeriod)
        {
            currentTime -= damagePeriod;
            player.hp -= damage;
            SetPlayerHPText();
        }
    }

    private void SetPlayerHPText()
    {
        PlayerHpText.text = $"Player HP : {player.hp}";
    }
}
