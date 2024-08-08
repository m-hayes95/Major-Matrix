using System.Collections;
using UnityEngine;

public class AttackCooldown : MonoBehaviour
{
    // Attack cooldown after an attack occurs
    public delegate void AttackReset();
    public static AttackReset OnNormalAttackReset, OnSpecialAttackReset;
    private bool canNormalAttack, canSpecialAttack;
    // Getters
    public bool GetCanNormalAttack() { return canNormalAttack; }
    public bool GetCanSpecialAttack() { return canSpecialAttack; }
    private void Start()
    {
        canNormalAttack = true;
        canSpecialAttack = true;
    }
    // Reset Normal range and melee attacks
    public void ResetNormalAttack(float cooldownTimer)
    {
        canNormalAttack = false;
        StartCoroutine(ResetNormalAttackCooldown(cooldownTimer));
    }
    private IEnumerator ResetNormalAttackCooldown(float cooldownTimer)
    {
        yield return new WaitForSeconds(cooldownTimer);
        Debug.Log("Enemy normal attack reset");
        canNormalAttack = true;
        OnNormalAttackReset?.Invoke();
    }
    // Reset Special attacks
    public void ResetSpecialAttack(float cooldownTimer)
    {
        canSpecialAttack = false;
        StartCoroutine(ResetSpecialAttackCooldown(cooldownTimer));
    }
    private IEnumerator ResetSpecialAttackCooldown(float cooldownTimer)
    {
        yield return new WaitForSeconds(cooldownTimer);
        Debug.Log("Enemy special attack reset");
        canSpecialAttack = true;
        OnSpecialAttackReset?.Invoke();
    }
}
