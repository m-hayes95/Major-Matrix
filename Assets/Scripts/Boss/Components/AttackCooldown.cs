using System.Collections;
using UnityEngine;

public class AttackCooldown : MonoBehaviour
{
    public delegate void AttackReset();
    public static AttackReset OnNormalAttackReset, OnSpecialAttackReset;
    private bool canNormalAttack, canSpecialAttack;
    public bool GetCanNormalAttack() { return canNormalAttack; }
    public bool GetCanSpecialAttack() { return canSpecialAttack; }

    private void Start()
    {
        canNormalAttack = true;
        canSpecialAttack = true;
    }
    // Normal range and melee attacks
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
    // Special attacks
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
