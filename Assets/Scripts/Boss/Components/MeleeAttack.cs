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
 
    private bool canAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackCooldown = GetComponent<AttackCooldown>();
    }
    private void OnEnable() { AttackCooldown.OnNormalAttackReset += ResetAttackValues; }
    private void OnDisable () { AttackCooldown.OnNormalAttackReset -= ResetAttackValues; }
    public void UseMeleeAttack(float damageAmount, float meleeAttackCooldownTimer)
    {
        // Attack the player if they get too close
        if (canAttack && playerHP && !playerHP.GetIsDead())
        {
            animator.SetBool("IsUsingMeleeAttack", true);
            meleeAttackSound.Play();
            canAttack = false;
            playerHP.DamagePlayer(damageAmount);
            attackCooldown.ResetNormalAttack(meleeAttackCooldownTimer);
        }
    }
    private void ResetAttackValues()
    {
        canAttack = true;
        animator.SetBool("IsUsingMeleeAttack", false);
    }
}
