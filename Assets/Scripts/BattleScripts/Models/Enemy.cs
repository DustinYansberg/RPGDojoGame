using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string Name = "Enemy";
    public int Health = 100;
    public int MaxHealth = 100;
    public Attack Kick = new("Kick", 10);
    public Attack Elemental = new("Elemental", 20);

    public List<Attack> Attacks = new(){
        new Attack("Kick", 10),
        new Attack("Elemental", 20)
    };

    public Attack RandomAttack()
    {
        return Attacks[Random.Range(0, Attacks.Count)];
    }

    public virtual void PerformAttack(Player Target, Attack ChosenAttack)
    {

        // Write some logic here to reduce the Targets health by your Attack's DamageAmount
        Target.Health -= ChosenAttack.DamageAmount;
        Debug.Log($"{Name} attacks {Target.Name} with {ChosenAttack.Name}, dealing {ChosenAttack.DamageAmount} damage and reducing {Target.Name}'s health to {Target.Health}!!");
    }
}
