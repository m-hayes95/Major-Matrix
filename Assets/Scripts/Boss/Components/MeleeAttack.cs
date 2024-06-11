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
    private BossStatsScriptableObject stats;
    private Animator animator;
 
    private bool canAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<BossStatsComponent>().bossStats;
        attackCooldown = GetComponent<AttackCooldown>();
    }
    private void OnEnable()
    {
        AttackCooldown.OnAttackReset += ResetAttackValues;
    }
    public void UseMeleeAttack()
    {
        // Attack the player if they get too close
        if (canAttack && playerHP && !playerHP.GetIsDead())
        {
            animator.SetBool("IsUsingMeleeAttack", true);
            meleeAttackSound.Play();
            canAttack = false;
            playerHP.DamagePlayer(stats.normalAttackDamage);
            attackCooldown.ResetAttack(stats.resetAttackTimer);
        }
    }

    private void ResetAttackValues()
    {
        canAttack = true;
        animator.SetBool("IsUsingMeleeAttack", false);
    }

    private void OnDisable()
    {
        AttackCooldown.OnAttackReset -= ResetAttackValues;
    }

}
