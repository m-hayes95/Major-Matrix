using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField, Tooltip("Add the player stats scriptable object in this field.")] 
    private PlayerStatsScriptableObject playerStats;
    [SerializeField, Tooltip("Add the boss stats scriptable object in this field.")] 
    private BossStatsScriptableObject bossStats;
    [SerializeField, Tooltip("Tick yes if this component is attached to a special attack.")] 
    private bool isSpecialAttack;
    [SerializeField] private GameObject impactEffect;
    private Rigidbody2D rb;
    private Vector2 savedVelocity;
    private bool isGamePaused = false;
    private bool doOnce = false;

    private float bossDamageAmount, playerDamageAmount;
    private const string DESPAWN = "DespawnGameobject";

    private void Start()
    {
        if (!isSpecialAttack) bossDamageAmount = bossStats.normalAttackDamage;
        else if (isSpecialAttack) bossDamageAmount = bossStats.specialAttackDamage;
        playerDamageAmount = playerStats.weaponPower;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.GetComponent<PlayerHealth>())
            PlayerCollisions(collision);
        if (collision != null && collision.gameObject.GetComponent<BossHealth>() && !isSpecialAttack)
            BossCollisions(collision);
        if (collision != null && collision.gameObject.GetComponent<PlatformTag>())
            PlatformCollisions();
        if (!isSpecialAttack) 
            Invoke(DESPAWN, 3f);
    }

    private void PlayerCollisions(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(bossDamageAmount);
        ImpactEffect();
        Invoke(DESPAWN, .1f);
    }
    private void BossCollisions(Collision2D collision)
    {
        collision.gameObject.GetComponent<BossHealth>().DamageBoss(playerDamageAmount);
        ImpactEffect();
        Invoke(DESPAWN, .1f);
    }
    private void PlatformCollisions()
    {
        ImpactEffect();
        Invoke(DESPAWN, .1f);
    }

    private void DespawnGameobject()
    {   
        gameObject.SetActive(false);
    }

    private void ImpactEffect()
    {
        GameObject newEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(newEffect, .2f);
    }
}
