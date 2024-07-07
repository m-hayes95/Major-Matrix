using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(
    typeof(Animator),
    typeof(BossStatsComponent),
    typeof(AttackCooldown)
)]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHP;
    [SerializeField] private AudioSource meleeAttackSound;
    private AttackCooldown attackCooldown;
    private Animator animator;
    private BossType bossType;
    private BossStatsScriptableObject stats;
    private bool canAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackCooldown = GetComponent<AttackCooldown>();
        bossType = GetComponent<BossType>();
        stats = GetComponent<BossStatsComponent>().bossStats;
    }
    private void OnEnable() { AttackCooldown.OnNormalAttackReset += ResetAttackValues; }
    private void OnDisable () { AttackCooldown.OnNormalAttackReset -= ResetAttackValues; }
    public void UseMeleeAttack(float damageAmount, float meleeAttackCooldownTimer)
    {
        // Attack the player if they get too close
        if (canAttack && playerHP && !playerHP.GetIsDead())
        {
            ScreenShake.Instance.ShakeCamera(
                stats.meleeAttackScreenShakeIntensity, 
                stats.meleeAttackScreenShakeTimer
                );
            if (bossType.CheckIfBossHasBT()) SavedStats.Instance.StoreTimesUsedMeleeBT();
            else SavedStats.Instance.StoreTimesUsedMeleeSM();
            animator.SetTrigger("BossUsedMeleeAttack");
            meleeAttackSound.Play();
            canAttack = false;
            playerHP.DamagePlayer(damageAmount);
            attackCooldown.ResetNormalAttack(meleeAttackCooldownTimer);
        }
    }
    private void ResetAttackValues()
    {
        canAttack = true;
    }
}
