using System.Collections;
using UnityEngine;

public abstract class BossAI : MonoBehaviour
{
    //Boss stats
    protected BossStatsScriptableObject stats;
    // References
    protected GameObject player;
    protected float distanceFromPlayer;
    private PlayerHealth playerHP;
    // Do once
    protected bool canAttack = true;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        player = FindObjectOfType<PlayerController>().gameObject;
        playerHP = player.GetComponent<PlayerHealth>();
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            distanceFromPlayer =
            Vector2.Distance(transform.position, player.transform.position);
            Debug.Log($" The boss is {distanceFromPlayer} from the player");
        }
        else
            Debug.LogWarning("Player object not found in BossAI script.");

        FacePlayer();
        UseSpecialAttack();

        Debug.Log($"AZ: Distance Y: {DistanceY()}....... playerY: {player.transform.position.y} - bossY {transform.position.y}");
        Debug.Log($"BZ: Vector Distance Method = {Vector2.Distance(transform.position, player.transform.position)}");
    }
    
    private void FacePlayer()
    {
        // Look towards the player depending on X pos
        float xDistanceFromPlayer = transform.position.x - player.transform.position.x;
        if (player != null)   
        {
            if (transform.position.x - player.transform.position.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1,1, 1);
        }
    }
    protected float DistanceY()
    {
        // Check distance between Y pos for boss and player, boss always 0
        float distanceY = player.transform.position.y - transform.position.y;
        return distanceY;
    }
    protected void Move()
    {
       // Move the boss character
    }
    protected bool UseSpecialAttack()
    {
        // Uses random chance to decide if the boss should use a normal or special attack
        bool useSpecialAttack;
        float rand = Random.value;
        if (rand < stats.chanceToUseSpecialAttack) useSpecialAttack = true;
        else useSpecialAttack = false;
        Debug.Log($"Random Value is {rand} use special attack = {useSpecialAttack}");
        return useSpecialAttack;
    }

    protected void NormalRangeAttack()
    {
        // Attack the player if they cross a certain distance
        if (canAttack && playerHP)
        {
            Debug.Log($"{gameObject.name} attacked {player.name} with a normal ranged attack - {stats.normalAttackDamage} HP");
            canAttack = false;
            playerHP.DamagePlayer(stats.normalAttackDamage);
            StartCoroutine(ResetAttack(stats.resetAttackTimer));
            // Play animation for attack
        }
    }
    protected void NormalCloseAttack()
    {
        // Attack the player if they get too close
        if (canAttack && playerHP)
        {
            Debug.Log($"{gameObject.name} attacked {player.name} with a normal close attack - {stats.normalAttackDamage} HP");
            canAttack = false;
            playerHP.DamagePlayer(stats.normalAttackDamage);
            StartCoroutine(ResetAttack(stats.resetAttackTimer));
            // Play animation for attack
        }
    }

    protected void SpecialLowAttack()
    {
        // Cross Screen attack from the ground
        if (canAttack)
        {
            Debug.Log($"{gameObject.name} attacked {player.name} with a special low attack - {stats.specialAttackDamage} HP");
            canAttack = false;
            StartCoroutine (ResetAttack(stats.resetAttackTimer));
            // Play animation for attack
        }
    }
    protected void SpecialHighAttack()
    {
        // Cross screen attack from the ceiling
        Debug.Log($"{gameObject.name} attacked {player.name} with a special high attack - {stats.specialAttackDamage} HP");
        canAttack = false;
        StartCoroutine(ResetAttack(stats.resetAttackTimer));
        // Play animation for attack
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
        // Low or High Speial attack zones
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(transform.position.x -20, transform.position.y + stats.specialHighAttackMinY, 0),
            new Vector3(20, transform.position.y + stats.specialHighAttackMinY, 0)
            );
    }

}
