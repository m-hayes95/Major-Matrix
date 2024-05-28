using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeicalAttackLow : BossAI
{
    [SerializeField] GameObject specialAttackGameObject;
    private Vector3 leftPosition, rightPosition;
    public UnityEvent OnAttackFinished;
    private List<GameObject> attacks;

    private void Start()
    {
        if (OnAttackFinished == null)
            OnAttackFinished = new UnityEvent();
        OnAttackFinished.AddListener(AttackFinished);
        // Set vector positions for the first attack 
        leftPosition = transform.position + Vector3.down * stats.offsetY;
        rightPosition = transform.position + Vector3.down * stats.offsetY;
    }

    public IEnumerator ExecuteSpecialAttack(float seconds)
    {
        attacks = new List<GameObject>();
        for (int i = 0; i < stats.numberOfAttacks; ++i)
        {
            SpawnLeft();
            SpawnRight();
            yield return new WaitForSeconds(seconds);
        }
        OnAttackFinished.Invoke();
    }

    private void SpawnLeft()
    {
        leftPosition += new Vector3(-stats.spacingX, stats.offsetY, 0);
        GameObject newAttack = Instantiate(
            specialAttackGameObject, leftPosition, Quaternion.identity
            );
        attacks.Add(newAttack);
    }

    private void SpawnRight()
    {
        rightPosition += new Vector3(stats.spacingX, stats.offsetY, 0);
        GameObject newAttack = Instantiate(
            specialAttackGameObject, rightPosition, Quaternion.identity
            );
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
        leftPosition = transform.position + Vector3.down * stats.offsetY;
        rightPosition = transform.position + Vector3.down * stats.offsetY;
    }
}
