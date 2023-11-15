using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Unit PlayerUnit;
    Unit EnemyUnit;

    public HealthBar PlayerHealthBar;
    public HealthBar EnemyHealthBar;

    public BattleState State;
    void Start()
    {
        State = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, new Vector3(-5, -1, 0), Quaternion.identity);
        PlayerUnit = playerGO.GetComponent<Unit>();
        PlayerHealthBar = playerGO.GetComponentInChildren<HealthBar>();
        PlayerUnit.healthBar = PlayerHealthBar;


        GameObject enemyGO = Instantiate(enemyPrefab, new Vector3(5, -1, 0), Quaternion.identity);
        EnemyUnit = enemyGO.GetComponent<Unit>();
        EnemyHealthBar = enemyGO.GetComponentInChildren<HealthBar>();
        EnemyUnit.healthBar = EnemyHealthBar;

        PlayerHealthBar.SetMaxHealth(PlayerUnit);
        EnemyHealthBar.SetMaxHealth(EnemyUnit);

        Debug.Log("BattleSystem: SetupBattle - " + PlayerUnit.UnitName + " vs " + EnemyUnit.UnitName);

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("BattleSystem: Choose an attack.");
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("BattleSystem: Enemy's turn.");
        Debug.Log($"{EnemyUnit.UnitName} attacks!");

        yield return new WaitForSeconds(1f);

        bool isDead = PlayerUnit.TakeDamage(EnemyUnit.Damage);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            State = BattleState.LOST;
            EndBattle();
        }
        else
        {
            State = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = EnemyUnit.TakeDamage(PlayerUnit.Damage);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            State = BattleState.WON;
            EndBattle();
        }
        else
        {
            State = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnAttackButton()
    {
        if (State != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());

    }

    public void OnHealButton()
    {
        if (State != BattleState.PLAYERTURN)
        {
            return;
        }
        PlayerUnit.CurrentHP += 10;
        PlayerHealthBar.SetHealth(PlayerUnit.CurrentHP);
        State = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    void EndBattle()
    {
        if (State == BattleState.WON)
        {
            Debug.Log("BattleSystem: You won the battle!");
        }
        else if (State == BattleState.LOST)
        {
            Debug.Log("BattleSystem: You lost the battle!");
        }
    }

}
