using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Animator playerAnime;
    public Animator enemyAnime;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject boltPrefab;
    public GameObject deathScreenPreFab;


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
        playerAnime = playerGO.GetComponent<Animator>();


        GameObject enemyGO = Instantiate(enemyPrefab, new Vector3(5, -1, 0), Quaternion.identity);
        EnemyUnit = enemyGO.GetComponent<Unit>();
        EnemyHealthBar = enemyGO.GetComponentInChildren<HealthBar>();
        EnemyUnit.healthBar = EnemyHealthBar;
        enemyAnime = enemyGO.GetComponent<Animator>();

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

        enemyAnime.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);

        bool isDead = PlayerUnit.TakeBaseAttackDamage(EnemyUnit.Damage);
        if (isDead)
        {
            playerAnime.SetTrigger("Death");
        }

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
        playerAnime.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        bool isDead = EnemyUnit.TakeBaseAttackDamage(PlayerUnit.Damage);
        if (isDead)
        {
            enemyAnime.SetTrigger("Death");
        }
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

    IEnumerator PlayerFireBall()
    {
        playerAnime.SetTrigger("FireBall");

        // ? wait for animation to finish before moving on to apply damage
        yield return new WaitForSeconds(2.25f);

        bool isDead = EnemyUnit.TakeFireBallAttackDamage(20);
        if (isDead)
        {
            enemyAnime.SetTrigger("Death");
        }
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

    // public void DestroyBolt()
    // {
    //     Destroy(GameObject.Find("bolt"));
    // }

    IEnumerator PlayerHeal()
    {
        playerAnime.SetTrigger("Heal");
        yield return new WaitForSeconds(2f);

        PlayerUnit.CurrentHP += 10;
        PlayerHealthBar.SetHealth(PlayerUnit.CurrentHP);
        State = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (State != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());

    }

    public void OnFireBallButton()
    {
        if (State != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerFireBall());
    }

    public void OnHealButton()
    {
        if (State != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerHeal());
    }


    void EndBattle()
    {
        if (State == BattleState.WON)
        {
            LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
            levelLoader.LoadNextLevel();
            Debug.Log("BattleSystem: You won the battle!");
        }
        else if (State == BattleState.LOST)
        {
            DestroyEverything();
            GameObject deathScreen = Instantiate(deathScreenPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            // GameObject playerGO = GameObject.Find("Player(Clone)");
            // Vector3 targetPosition = new(0, 0, -1);
            // float t = 0f;
            // while (t <= 1.0)
            // {
            //     t += Time.deltaTime / .5f;
            //     playerGO.transform.position = Vector3.Lerp(playerGO.transform.position, targetPosition, Mathf.SmoothStep(0f, 1f, 1f));
            // }
            Camera mainCamera = Camera.main;
            mainCamera.backgroundColor = new Color(0, 0, 0, 1);
            Debug.Log("BattleSystem: You lost the battle!");
            // StartCoroutine(MoveToCenter(playerGO));
        }
    }


    // IEnumerator MoveToCenter(GameObject go)
    // {
    //     Debug.Log($"BattleSystem: Moving {go.name} from {go.transform.position} to screen center");
    //     float t = 0f;
    //     Vector3 targetPosition = new(0, 0, -1);
    //     while (t <= 1.0)
    //     {
    //         t += Time.deltaTime / .5f;
    //         go.transform.position = Vector3.Lerp(go.transform.position, targetPosition, Mathf.SmoothStep(0f, 1f, t));
    //         // go.transform.position = targetPosition;
    //         yield return 1f;

    //     }
    // }
    // IEnumerator MoveToCenter(GameObject go)
    // {
    //     Debug.Log($"BattleSystem: Moving {go.name} tofrom {go.transform.position} to (0, 0, {go.transform.position.z})");
    //     float t = 0f;
    //     while (t <= 1.0)
    //     {
    //         t += Time.deltaTime / .5f;
    //         go.transform.position = Vector3.Lerp(go.transform.position, new Vector3(0, 0, go.transform.position.z), Mathf.SmoothStep(0f, 1f, t));
    //         yield return null;
    //     }
    // }

    public void DestroyEverything()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            Debug.Log("BattleSystem: Destroying " + go.name);
            if (go.name != "Player(Clone)" && go.name != "Main Camera" && go.activeInHierarchy)
            {
                Destroy(go);
            }
        }
    }
}
