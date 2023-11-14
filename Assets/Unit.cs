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

    public bool TakeDamage(int damageAmount)
    {
        CurrentHP -= damageAmount;

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
