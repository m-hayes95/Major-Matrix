using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossAI : MonoBehaviour
{
    //Boss stats
    protected BossStatsScriptableObject stats;

    // References
    protected GameObject player;
    protected float distanceFromPlayer;
    protected BossHealth bossHP;
    protected Shield shield;

    private PlayerHealth playerHP;
    private Shoot shoot;
    private SpeicalAttacks specialAttacks;

    // Special Attack Selector
    private int lowSpecialAttack = 0;
    private int highSpecialAttack = 1;

    // Shooting Direction
    private Transform target;

    // Do once
    protected bool canAttack = true;
    protected bool canChase = true;
    protected bool usingSpecialAttack = false;
    private bool facingLeft = true;
    protected bool isGamePaused = false;

    private void OnEnable() { GameManager.OnPaused += UpdateIsGamePaused; }
    private void OnDisable() { GameManager.OnPaused -= UpdateIsGamePaused; }
    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        player = FindObjectOfType<PlayerController>().gameObject;
        playerHP = player.GetComponent<PlayerHealth>();
        bossHP = GetComponent<BossHealth>();
        shield = GetComponent<Shield>();
        shoot = GetComponent<Shoot>();
        specialAttacks = GetComponent<SpeicalAttacks>();
        // Dont allow interuptions for special attacks
        if (specialAttacks.OnAttackFinished == null)
            specialAttacks.OnAttackFinished = new UnityEvent();
        specialAttacks.OnAttackFinished.AddListener(ResetSpecialAttackBool);
    }

    protected virtual void Update()
    {
        if (!bossHP.GetIsDead() && !isGamePaused)   
        {
            target = player.transform;

            if (player != null)
            {
                distanceFromPlayer =
                Vector2.Distance(transform.position, player.transform.position);
                Debug.Log($" The boss is {distanceFromPlayer} from the player");
            }
            else
                Debug.LogWarning("Player object not found in BossAI script.");

            CheckWhichSidePlayerIsOn();


            Debug.Log($"AZ: Distance Y: {DistanceY()}....... playerY: {player.transform.position.y} - bossY {transform.position.y}");
            Debug.Log($"BZ: Vector Distance Method = {Vector2.Distance(transform.position, player.transform.position)}");
        }
    }

    private void UpdateIsGamePaused()
    {
        isGamePaused = !isGamePaused;
    }

    private void CheckWhichSidePlayerIsOn()
    {
        float xDistanceFromPlayer = transform.position.x - player.transform.position.x;
        if (!facingLeft && xDistanceFromPlayer > 0)
        {
            FacePlayer();
        }
        if (facingLeft && xDistanceFromPlayer < 0)
        {
            FacePlayer();
        }
    }
    
    private void FacePlayer()
    {
        // Look towards the player depending on X pos
        facingLeft = !facingLeft;
        transform.Rotate(0f, -180f, 0f);
    }
    protected float DistanceY()
    {
        // Check distance between Y pos for boss and player, boss always 0
        float distanceY = player.transform.position.y - transform.position.y;
        Debug.Log($"Y distance from player {distanceY}");
        return distanceY;
    }
    protected void MoveToPlayer()
    {
        // Move the boss character when the player is in the safe zone
        canChase = false;
        if (player != null)
        {
            Vector2 moveDir = new Vector2(transform.position.x - player.transform.position.x, 0);
            transform.Translate(moveDir * stats.moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Player is not assigned!");
        }
        StartCoroutine(ChaseCooldown(stats.chaseCooldown));
        
    }

    private IEnumerator ChaseCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canChase = !canChase;
    }
    protected bool RandomChance(float chanceModifier)
    {
        // Uses random chance to decide if the boss should use a normal or special attack
        bool randomChance;
        float rand = Random.value;
        if (rand < chanceModifier) randomChance = true;
        else randomChance = false;
        Debug.Log($"Random Value is {rand} random chance = {randomChance}");
        return randomChance;
    }

    protected void NormalRangeAttack()
    {
        // Attack the player if they cross a certain distance
        if (canAttack && !usingSpecialAttack)
        {
            shoot.FireWeaponBoss(target, stats.shotFoce);
            Debug.Log($"{gameObject.name} attacked {player.name} with a normal ranged attack - {stats.normalAttackDamage} HP");
            canAttack = false;
            //playerHP.DamagePlayer(stats.normalAttackDamage);
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
        if (canAttack && !usingSpecialAttack)
        {
            usingSpecialAttack = true;
            specialAttacks.CallSpecialAttackLowOrHigh(lowSpecialAttack);
            Debug.Log($"{gameObject.name} attacked {player.name} with a special low attack - {stats.specialAttackDamage} HP");
            canAttack = false;
            StartCoroutine (ResetAttack(stats.resetAttackTimer));
            // Play animation for attack
        }
    }
    protected void SpecialHighAttack()
    {
        // Cross screen attack from the ceiling
        if (canAttack && !usingSpecialAttack)
        {
            usingSpecialAttack = true;
            specialAttacks.CallSpecialAttackLowOrHigh(highSpecialAttack);
            Debug.Log($"{gameObject.name} attacked {player.name} with a special high attack - {stats.specialAttackDamage} HP");
            canAttack = false;
            StartCoroutine(ResetAttack(stats.resetAttackTimer));
        }
        // Play animation for attack
    }

    private void ResetSpecialAttackBool()
    {
        usingSpecialAttack = false; 
    }
    protected void Shield()
    {
        shield.UseShield();
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
