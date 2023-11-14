using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private string _Name;
    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    private int _DamageAmount;
    public int DamageAmount
    {
        get { return _DamageAmount; }
        set { _DamageAmount = value; }
    }

    public Attack(string name, int damageAmount)
    {
        _Name = name;
        DamageAmount = damageAmount;
    }
}
