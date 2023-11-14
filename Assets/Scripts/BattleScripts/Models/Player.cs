using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player
{
    public string Name = "Player";
    public int Health = 100;
    public int MaxHealth = 100;


    public List<Attack> Attacks = new(){
        new Attack("Punch", 10),
        new Attack("Fireball", 20),
    };

    public virtual void PerformAttack(Enemy Target, Attack ChosenAttack)
    {

        // Write some logic here to reduce the Targets health by your Attack's DamageAmount
        Target.Health -= ChosenAttack.DamageAmount;
        Debug.Log($"{Name} attacks {Target.Name} with {ChosenAttack.Name}, dealing {ChosenAttack.DamageAmount} damage and reducing {Target.Name}'s health to {Target.Health}!!");
    }

    public void HealSelf()
    {
        // Write some logic here to increase the Players health by a random amount between 10 and 15
        int rand = Random.Range(7, 15);
        Health += rand;
        Debug.Log($"{Name} heals themselves for {rand} health, bringing their health to {Health}!!");
    }
}
