using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField, Tooltip("Add the player stats scriptable object in this field.")] 
    private PlayerStatsScriptableObject playerStats;
    [SerializeField, Tooltip("Add the boss stats scriptable object in this field.")] 
    private BossStatsScriptableObject bossStats;
    [SerializeField, Tooltip("Tick yes if this component is attached to a special attack.")] 
    private bool isSpecialAttack;

    private float damageAmount;
    private const string DESTROY = "DestroyBullet";

    private void Start()
    {
        if (!isSpecialAttack) damageAmount = bossStats.normalAttackDamage;
        else if (isSpecialAttack) damageAmount = bossStats.specialAttackDamage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(damageAmount);
            if (!isSpecialAttack) Invoke(DESTROY, .1f);
        }
        else if (collision.gameObject.GetComponent<BossHealth>())
        {
            // Damage boss
            Invoke(DESTROY, .1f);
        }
        if (!isSpecialAttack) Invoke(DESTROY, 3f);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
