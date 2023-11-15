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

    public bool TakeDamage(int damageAmount)
    {
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
