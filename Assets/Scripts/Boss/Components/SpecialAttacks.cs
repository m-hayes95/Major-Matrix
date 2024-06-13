using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AttackCooldown))]
public class SpecialAttacks : MonoBehaviour
{
    [SerializeField] private GameObject specialAttackGameObject;
    //[SerializeField] Transform highAttackPosistion
    [SerializeField] private AudioSource lowAttackSound, highAttackSound;
    private AttackCooldown attackCooldown;
    private BossStatsScriptableObject stats;
    private Vector3 leftPositionLow, rightPositionLow;
    private Vector3 leftPositionHigh, rightPositionHigh;
    public UnityEvent OnAttackFinished;
    private List<GameObject> attacks;
    private bool inSpecialAttack = false;
    private bool canAttack = true;
    private void OnEnable() { AttackCooldown.OnSpecialAttackReset += ResetAttackBool; }
    private void OnDisable() { AttackCooldown.OnSpecialAttackReset -= ResetAttackBool; }

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        attackCooldown = GetComponent<AttackCooldown>();    
    }
    public bool GetIsInSpecialAttack() {  return inSpecialAttack; }

    private void Start()
    {
        if (OnAttackFinished == null)
            OnAttackFinished = new UnityEvent();
        OnAttackFinished.AddListener(AttackFinished);
        // Set vector positions for the first attack (low special attack)
        leftPositionLow = transform.position + Vector3.down * stats.offsetY;
        rightPositionLow = transform.position + Vector3.down * stats.offsetY;
        // Set vector positions for the first attack (high special attack)
        leftPositionHigh = transform.position + Vector3.up * stats.spawnHeight;
        rightPositionHigh = transform.position + Vector3.up * stats.spawnHeight;
    }
    private void ResetAttackBool() // can attack gets reset after cooldown
    {
        canAttack = true; 
    }
    public void CallSpecialAttackLowOrHigh(int lowOrHigh)
    {
        if (!inSpecialAttack && canAttack)
        {
            canAttack = false;
            inSpecialAttack = true;
            Mathf.Clamp(lowOrHigh, 0, 1);
            if (lowOrHigh != 0 || lowOrHigh != 1)
                Debug.LogWarning($"The current arguent: {lowOrHigh} is not valid. The attack for the call speical attack low or high method requies a 0 (low attack) or 1 (high attack).");
            StartCoroutine(ExecuteSpecialAttack(lowOrHigh));
        }
    }
    private  IEnumerator ExecuteSpecialAttack(int lowOrHigh)
    {
        attacks = new List<GameObject>();
        for (int i = 0; i < stats.numberOfAttacks; ++i)
        {
            SpawnLeft(lowOrHigh);
            SpawnRight(lowOrHigh);
            yield return new WaitForSeconds(stats.timeBetweenAttacks);
        }
        OnAttackFinished.Invoke();
    }

    private void SpawnLeft(int lowOrHigh)
    {
        Vector3 spawnPosition = Vector3.zero;
        Vector3 scale = new Vector3(1, 1, 1);
        if (lowOrHigh == 0) // Low Special Attack
        {
            spawnPosition = leftPositionLow += new Vector3(-stats.spacingX, stats.offsetY, 0);
            lowAttackSound.Play();
        }
        else if (lowOrHigh == 1) // High Special Attack
        { 
            spawnPosition = leftPositionHigh += new Vector3(-stats.spacingX, stats.offsetY, 0);
            scale = new Vector3(1, -1, 1);
            highAttackSound.Play();
        }

        InstatiateNewAttack(spawnPosition, scale, lowOrHigh);
    }

    private void SpawnRight(int lowOrHigh)
    {
        Vector3 spawnPosition = Vector3.zero;
        Vector3 scale = new Vector3(1, 1, 1); // Set to default
        if (lowOrHigh == 0) // Low Special Attack
        {
            spawnPosition = rightPositionLow += new Vector3(stats.spacingX, stats.offsetY, 0);
            lowAttackSound.Play();
        }
        else if (lowOrHigh == 1) // High Special Attack
        {
            spawnPosition = rightPositionHigh += new Vector3(stats.spacingX, stats.offsetY, 0);
            scale = new Vector3(1, -1, 1);
            highAttackSound.Play(); 
        };

        InstatiateNewAttack(spawnPosition, scale, lowOrHigh);
    }

    private void InstatiateNewAttack(Vector3 spawnVector, Vector3 scaleVector, int lowOrHigh)
    {
        GameObject newAttack = Instantiate(
            specialAttackGameObject, spawnVector, Quaternion.identity
            );
        newAttack.transform.localScale = scaleVector;
        attacks.Add(newAttack);
        if (lowOrHigh == 1) DropHighAttacks(newAttack);
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
        for (int x = 0; x < attacks.Count; ++x)
        {
            attacks[x].SetActive(false);
        }
        Debug.Log("Special Attack List Cleared");
        // Reset Positions of initial vectors
        leftPositionLow = transform.position + Vector3.down * stats.offsetY;
        rightPositionLow = transform.position + Vector3.down * stats.offsetY;
        leftPositionHigh = transform.position + Vector3.up * stats.spawnHeight;
        rightPositionHigh = transform.position + Vector3.up * stats.spawnHeight;
    }
}
