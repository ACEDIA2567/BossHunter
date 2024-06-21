using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadeManager : Singleton<RadeManager>
{
    public Minotaur minotaur;
    public Player player;

    public void DamageToBoss()
    {
        float playerPower = player.m_attackPower;
        float bossDefensePower = minotaur.defensePower;
        float damage = CalculateDamage(playerPower, bossDefensePower);
        minotaur._curHp -= damage;
    }

    public void DamageToPlayer(float additionalDamage, bool isBlock)
    {
        float bossPower = minotaur.attackPower;
        float damage = CalculateDamage(bossPower, 0);
        damage *= additionalDamage;
        if(isBlock )
        {
            damage = damage * 0.2f;
        }
        player.hp -= damage;

        // player hurt animation
        if (player.hp > 0)
        {
            player.HurtAnimation();
        }
    }

    private float CalculateDamage(float attackPower, float defensePower)
    {
        float minAttackPower = attackPower * 0.9f;
        float maxAttackPower = attackPower * 1.1f;

        float randomAttackPower = Random.Range(minAttackPower, maxAttackPower);

        float damageReduction = defensePower / 100f;
        float damage = randomAttackPower * (1 - damageReduction);

        return damage;
    }
}
