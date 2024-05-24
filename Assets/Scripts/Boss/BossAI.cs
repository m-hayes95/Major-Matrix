using System.Collections;
using UnityEngine;

public abstract class BossAI : MonoBehaviour
{
    //Boss stats
    protected BossStatsScriptableObject stats;
    // Distance from player
    protected GameObject player;
    protected VectorDistanceChecker vectorDistance;
   
    protected float distanceFromPlayer;
    // Do once
    protected bool canAttack = true;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        player = FindObjectOfType<PlayerController>().gameObject;
        vectorDistance = GetComponent<VectorDistanceChecker>();
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            distanceFromPlayer =
            vectorDistance.CheckVector2DistanceBetweenAandB(gameObject, player);
            Debug.Log($" The boss is {distanceFromPlayer} from the player");
            Debug.Log($" Attack target is {player.name}");
        }
        else
            Debug.LogWarning("Player object not found in BossAI script.");
        FacePlayer();
    }
    private void FacePlayer()
    {
        // Look towards the player depending on X pos
        if (player != null)
        {
            if (transform.position.x - player.transform.position.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1,1, 1);
        }
    }
    protected void Move()
    {
       // Move the boss character
    }

    protected void NormalRangeAttack()
    {
        // Attack the player if they cross a certain distance
        if (canAttack)
        {
            Debug.Log($"{gameObject.name} attacked {player.name} with a normal ranged attack - {stats.normalAttackDamage} HP");
            canAttack = false;
            StartCoroutine(ResetAttack(.1f));
        }
    }
    protected void NormalCloseAttack()
    {
        // Attack the player if they get too close
        if (canAttack)
        {
            Debug.Log($"{gameObject.name} attacked {player.name} with a normal close attack - {stats.normalAttackDamage} HP");
            canAttack = false;
            StartCoroutine(ResetAttack(.1f));
        }
    }

    protected void SpecialLowAttack()
    {
        // Cross Screen attack from the ground
        if (canAttack)
        {
            Debug.Log($"{gameObject.name} attacked {player.name} with a special low attack - {stats.specialAttackDamage} HP");
            canAttack = false;
            StartCoroutine (ResetAttack(3));
        }
    }
    protected void SpecialHighAttack()
    {
        // Cross screen attack from the ceiling
        Debug.Log($"{gameObject.name} attacked {player.name} with a special high attack - {stats.specialAttackDamage} HP");
        canAttack = false;
        StartCoroutine(ResetAttack(3));
    }
    protected void Sheild()
    {
        // Make the boss resistant to player damage
    }

    private IEnumerator ResetAttack(float resetTimer)
    {
        yield return new WaitForSeconds( resetTimer );
        Debug.Log("Enemy attack reset");
        canAttack = !canAttack;
    }

    private void OnDrawGizmos()
    {
        // Close attack range
        Gizmos.color = new Color(1,0,0,0.15f);
        Gizmos.DrawSphere(transform.position, stats.closeRangeAttackThreshold);
        // Safe zone from attacks
        Gizmos.color = new Color(0, 1, 0, .1f);
        Gizmos.DrawSphere(transform.position, stats.longRangeAttackThreshold);
    }

}
