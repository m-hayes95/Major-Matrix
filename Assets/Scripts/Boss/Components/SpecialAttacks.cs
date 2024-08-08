using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(
    typeof(Animator),
    typeof(BossStatsComponent),
    typeof(AttackCooldown)
)]
public class SpecialAttacks : MonoBehaviour
{
    // Instiated Game Objects for attack
    [SerializeField, Tooltip("Add prefabs for special attacks here")] 
    private GameObject specialAttackGameObjectLow, specialAttackGameObjectHigh;
    // References
    [SerializeField, Tooltip("Add SFX sounds under sound heading here")] 
    private AudioSource lowAttackSound, highAttackSound;
    private AttackCooldown attackCooldown;
    private BossStatsScriptableObject stats;
    private Animator animator;
    private BossHealth health;
    private BossType bossType;
    // Spawn Positions for attacks
    private Vector3 leftPositionLow, rightPositionLow;
    private Vector3 leftPositionHigh, rightPositionHigh;
    // Events
    public UnityEvent OnAttackFinished;
    // Current Attacks
    private List<GameObject> attacks;
    // Checks
    private bool inSpecialAttack = false;
    private bool canAttack = true;
    
    private void OnEnable() { AttackCooldown.OnSpecialAttackReset += ResetAttackBool; }
    private void OnDisable() { AttackCooldown.OnSpecialAttackReset -= ResetAttackBool; }

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        attackCooldown = GetComponent<AttackCooldown>();    
        animator = GetComponent<Animator>();
        health = GetComponent<BossHealth>();
        bossType = GetComponent<BossType>();
    }
    public bool GetIsInSpecialAttack() {  return inSpecialAttack; }

    private void Start()
    {
        if (OnAttackFinished == null)
            OnAttackFinished = new UnityEvent();
        OnAttackFinished.AddListener(AttackFinished);
    }

    private void Update()
    {
        if (health.GetIsDead())
        {
            RemoveOnScreenAttacks();
        }
    }

    private void ResetAttackBool() // can attack gets reset after cooldown
    {
        canAttack = true; 
    }
    public void CallSpecialAttackLowOrHigh(int lowOrHigh, Transform currentPosition) 
    // Called by BT or FSM AI (0 = Low attack, 1 = high attack)
    {
        if (!inSpecialAttack && canAttack)
        {
            // Call screen shake
            ScreenShake.Instance.ShakeCamera(
                stats.specialAttackScreenShakeIntensity, stats.specialAttackScreenShakeTimer
                );
            // Store stats
            if (bossType.CheckIfBossHasBT()) SavedStats.Instance.StoreTimesUsedSpecialAttackBT();
            else SavedStats.Instance.StoreTimesUsedSpecialAttackSM();
            // Set start position and boolean checks
            SetStartPositions(currentPosition);
            canAttack = false;
            inSpecialAttack = true;
            // Choose high or low attack
            Mathf.Clamp(lowOrHigh, 0, 1);
            if (lowOrHigh == 0) animator.SetTrigger("BossUsedSpecialAttack_LOW");
            if (lowOrHigh == 1) animator.SetTrigger("BossUsedSpecialAttack_HIGH");
            StartCoroutine(ExecuteSpecialAttack(lowOrHigh));
        }
    }
    private void SetStartPositions(Transform transform)
    {
        // Set vector positions for the first attack (low special attack)
        leftPositionLow = transform.position + Vector3.down * stats.offsetY;
        rightPositionLow = transform.position + Vector3.down * stats.offsetY;
        // Set vector positions for the first attack (high special attack)
        leftPositionHigh = transform.position + Vector3.up * stats.spawnHeight;
        rightPositionHigh = transform.position + Vector3.up * stats.spawnHeight;
    }
    private  IEnumerator ExecuteSpecialAttack(int lowOrHigh)
    {
        // Spawn in attacks with a delay for each set
        attacks = new List<GameObject>();
        for (int i = 0; i < stats.numberOfAttacks; ++i)
        {
            SpawnLeft(lowOrHigh);
            SpawnRight(lowOrHigh);
            yield return new WaitForSeconds(stats.timeBetweenAttacks);
        }
        // Wait extra time for the last attacks to finish
        yield return new WaitForSeconds(0.8f);
        OnAttackFinished.Invoke();
    }

    private void SpawnLeft(int lowOrHigh) // Find position on the left side of boss 
    {
        Vector3 spawnPosition = Vector3.zero;
        if (lowOrHigh == 0) // Low Special Attack
        {
            spawnPosition = leftPositionLow += new Vector3(-stats.spacingX, stats.offsetY, 0);
            lowAttackSound.Play();
        }
        else if (lowOrHigh == 1) // High Special Attack
        { 
            spawnPosition = leftPositionHigh += new Vector3(-stats.spacingX, stats.offsetY, 0);
            highAttackSound.Play();
        }

        InstatiateNewAttack(spawnPosition, lowOrHigh);
    }

    private void SpawnRight(int lowOrHigh) // Find position on the right side of boss 
    {
        Vector3 spawnPosition = Vector3.zero;
        if (lowOrHigh == 0) // Low Special Attack
        {
            spawnPosition = rightPositionLow += new Vector3(stats.spacingX, stats.offsetY, 0);
            lowAttackSound.Play();
        }
        else if (lowOrHigh == 1) // High Special Attack
        {
            spawnPosition = rightPositionHigh += new Vector3(stats.spacingX, stats.offsetY, 0);
            highAttackSound.Play(); 
        };

        InstatiateNewAttack(spawnPosition, lowOrHigh);
    }

    private void InstatiateNewAttack(Vector3 spawnVector, int lowOrHigh) // Spawn the special attack
    {
        GameObject newAttack;
        if (lowOrHigh == 0) // Spawn low attack game objects 
        {
            newAttack = Instantiate(
                specialAttackGameObjectLow, spawnVector, Quaternion.identity
                );
            attacks.Add(newAttack);
        }
        if (lowOrHigh == 1) // Spawn high attack game objects 
        {
            newAttack = Instantiate(
                specialAttackGameObjectHigh, spawnVector, Quaternion.identity
                );
            attacks.Add(newAttack);
            DropHighAttacks(newAttack);
        }
    }

    private void DropHighAttacks(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 
            stats.highSpecialAttackGravityScale;
    }

    private void AttackFinished() 
    {
        inSpecialAttack = false;
        attackCooldown.ResetSpecialAttack(stats.resetSpecialAttackTimer);
        Debug.Log("Special Attack Finished");
        // Remove new gameobjects from scene
        RemoveOnScreenAttacks();
        Debug.Log("Special Attack List Cleared");
    }
    private void RemoveOnScreenAttacks()
    {
        for (int x = 0; x < attacks.Count; ++x)
        {
            attacks[x].SetActive(false);
        }
    }
}
