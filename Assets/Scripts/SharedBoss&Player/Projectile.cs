using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, Tooltip("Add the player stats scriptable object in this field.")] 
    private PlayerStatsScriptableObject playerStats;
    [SerializeField, Tooltip("Add the boss stats scriptable object in this field.")] 
    private BossStatsScriptableObject bossStats;
    private const string DESTROY = "DestroyBullet";
    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.GetComponent<PlayerHealth>())
            {
                collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(bossStats.normalAttackDamage);
                Invoke(DESTROY, .1f);
            }
            else if (collision.gameObject.GetComponent<BossHealth>())
            {
                // Damage boss
                Invoke(DESTROY, .1f);
            }
        }
        Invoke(DESTROY, 3f);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
