using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;
    [SerializeField] private Text PlayerHpText;
    [SerializeField] private Player player;

    [SerializeField] private Text EnemyHpText;
    [SerializeField] private Enemy enemy;

    private float damagePeriod = 5f;
    private float damage = 10f;
    private float currentTime = 0f;

    private void Awake()
    {
        Instance = this;
    }

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

    private void SetEnemyHPText()
    {
        EnemyHpText.text = $"Enemy HP : {enemy.hp}";
    }

    public void HitEnemy()
    {
        enemy.hp -= damage;
        SetEnemyHPText();
    }
}
