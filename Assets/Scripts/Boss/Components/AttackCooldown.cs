using System.Collections;
using UnityEngine;

public class AttackCooldown : MonoBehaviour
{
    public delegate void AttackReset();
    public static AttackReset OnAttackReset;
    private bool canAttack = true;
    public bool GetCanAttack() { return canAttack; }

    public void ResetAttack(float cooldownTimer)
    {
        canAttack = false;
        StartCoroutine(ResetAttackCooldown(cooldownTimer));
    }
    private IEnumerator ResetAttackCooldown(float cooldownTimer)
    {
        yield return new WaitForSeconds(cooldownTimer);
        Debug.Log("Enemy attack reset");
        canAttack = true;
        OnAttackReset?.Invoke();
    }
}
