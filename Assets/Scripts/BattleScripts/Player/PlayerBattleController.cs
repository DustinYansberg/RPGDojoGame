using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO this probably should be a more generic controller, 
// TODO  maybe attached to the UI since it will contain both enemy and player
public class PlayerBattleController : MonoBehaviour
{
    Player Player1 = new();
    Enemy Enemy1 = new();

    void Start()
    {
        Debug.Log("PlayerBattleController");
        Debug.Log($"PlayerBattleController: {Player1.Name} Has entered the battle!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Player1.PerformAttack(Enemy1, Player1.Attacks[0]);
            Enemy1.PerformAttack(Player1, Enemy1.RandomAttack());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Player1.PerformAttack(Enemy1, Player1.Attacks[1]);
            Enemy1.PerformAttack(Player1, Enemy1.RandomAttack());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Player1.HealSelf();
            Enemy1.PerformAttack(Player1, Enemy1.RandomAttack());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("PlayerBattleController: Defend");
        }
    }
}
