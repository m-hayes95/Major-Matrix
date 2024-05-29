using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeicalAttacks : BossAI
{
    [SerializeField] GameObject specialAttackGameObject;
    [SerializeField] Transform highAttackPosistion;
    private Vector3 leftPositionLow, rightPositionLow;
    private Vector3 leftPositionHigh, rightPositionHigh;
    public UnityEvent OnAttackFinished;
    private List<GameObject> attacks;

    private void Start()
    {
        if (OnAttackFinished == null)
            OnAttackFinished = new UnityEvent();
        OnAttackFinished.AddListener(AttackFinished);
        // Set vector positions for the first attack (low special attack)
        leftPositionLow = transform.position + Vector3.down * stats.offsetY;
        rightPositionLow = transform.position + Vector3.down * stats.offsetY;
        // Set vector positions for the first attack (high special attack)
        leftPositionHigh = transform.position + Vector3.up * 10f;
        rightPositionHigh = transform.position + Vector3.up * 10f;
    }
    public void CallSpecialAttackLowOrHigh(int lowOrHigh)
    {
        if (lowOrHigh != 0 || lowOrHigh != 1) 
            Debug.LogWarning($"The current arguent: {lowOrHigh} is not valid. The attack for the call speical attack low or high method requies a 0 (low attack) or 1 (high attack).");
        StartCoroutine(ExecuteSpecialAttack(lowOrHigh));
        // need y and y scale to pass in correct info
    }

    public IEnumerator ExecuteSpecialAttack(int lowOrHigh)
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
        }
        else if (lowOrHigh == 1) // High Special Attack
        { 
            spawnPosition = leftPositionHigh += new Vector3(-stats.spacingX, stats.offsetY, 0);
            scale = new Vector3(1, -1, 1);
        }
        
        GameObject newAttack = Instantiate(
            specialAttackGameObject, spawnPosition, Quaternion.identity
            );
        newAttack.transform.localScale = scale;
        attacks.Add(newAttack);
    }

    private void SpawnRight(int lowOrHigh)
    {
        Vector3 spawnPosition = Vector3.zero;
        Vector3 scale = new Vector3(1, 1, 1); // Set to default
        if (lowOrHigh == 0) // Low Special Attack
        {
            spawnPosition = rightPositionLow += new Vector3(stats.spacingX, stats.offsetY, 0);
        }
        else if (lowOrHigh == 1) // High Special Attack
        {
            spawnPosition = rightPositionHigh += new Vector3(stats.spacingX, stats.offsetY, 0);
            scale = new Vector3(1, -1, 1);
        };
        GameObject newAttack = Instantiate(
            specialAttackGameObject, spawnPosition, Quaternion.identity
            );
        newAttack.transform.localScale = scale;
        attacks.Add(newAttack);
    }

    private void AttackFinished()
    {
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
        leftPositionHigh = transform.position + Vector3.up * 10f;
        rightPositionHigh = transform.position + Vector3.up * 10f;
    }
}
