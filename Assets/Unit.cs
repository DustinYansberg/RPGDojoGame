using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName;
    public int UnitLevel;
    public int Damage;
    public int MaxHP;
    public int CurrentHP;

    public HealthBar healthBar;

    public bool TakeBaseAttackDamage(int damageAmount)
    {
        damageAmount = UnityEngine.Random.Range((int)(.75 * damageAmount), (int)(1.5 * damageAmount));
        CurrentHP -= damageAmount;
        healthBar.SetHealth(CurrentHP);

        if (CurrentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TakeFireBallAttackDamage(int damageAmount)
    {
        damageAmount = UnityEngine.Random.Range((int)(.75 * damageAmount), (int)(1.5 * damageAmount));
        CurrentHP -= damageAmount;
        healthBar.SetHealth(CurrentHP);

        if (CurrentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
