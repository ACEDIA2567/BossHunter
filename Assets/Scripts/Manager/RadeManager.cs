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
        int crit = Random.Range(0, 10);
        if (crit <= 1)
        {
            damage *= 2;
        }
        minotaur._curHp -= damage;
    }

    private void ReflectAttackToBoss()
    {
        float damage = 10000;
        minotaur._curHp -= damage;
        minotaur.defensePower -= 1;
    }

    public void DamageToPlayer(float additionalDamage, bool isBlock)
    {
        float bossPower = minotaur.attackPower;
        float damage = CalculateDamage(bossPower, 0);
        damage *= additionalDamage;
        if(isBlock )
        {
            if(player.m_blockKeepTime > 0 && player.m_blockKeepTime <= 0.15f)
            {
                damage = 0;
                ReflectAttackToBoss();
            }
            else
            {
                damage = damage * 0.2f;
            }
        }
        player.hp -= damage;
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
