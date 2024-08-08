using UnityEngine;

public class Shoot : MonoBehaviour
{
    // This script is used by both the player and boss AI
    [SerializeField, Tooltip("Add the bullet prefab to this field.")] 
    private GameObject bullet;
    [SerializeField, Tooltip("Add the spawn point transform, where the bullet will spawn from, to this field")] 
    private Transform spawnPoint;
    [SerializeField, Tooltip("Add the bullet sound to this field")] 
    private AudioSource bulletSound;
    [SerializeField, Tooltip("Add the Attack Cooldown script to this field (Only required for boss AI)")] 
    private AttackCooldown attackCooldown;
    private Transform newTarget;
    private GameObject newBullet;
    private Animator animator;
    private BossType bossType;
    private NewPlayerController playerRef;
    private bool canShoot = true;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossType = GetComponent<BossType>(); 
        playerRef = GetComponent<NewPlayerController>();
    }
    private void OnEnable()
    {
        AttackCooldown.OnNormalAttackReset += ResetCanAttackBool;
    }
    private void OnDisable()
    {
        AttackCooldown.OnNormalAttackReset -= ResetCanAttackBool;
    }
    // Called from Boss AI class
    public void FireWeaponBoss(Transform target, float velocity, float attackResetTimer)
    {
        if (canShoot)
        {
            if (bossType.CheckIfBossHasBT()) SavedStats.Instance.StoreTimesUsedShootBT();
            else SavedStats.Instance.StoreTimesUsedShootSM();
            animator.SetTrigger("BossUsedRangedAttack");
            canShoot = false;
            InstantiateNewBullet();
            newTarget = target;
            SetVelocityAndDir(velocity);
            attackCooldown.ResetNormalAttack(attackResetTimer);
        }
    }
    // Called from player input class
    public void FireWeaponPlayer(float velocity)
    {
        if (playerRef != null)
        {
            ScreenShake.Instance.ShakeCamera(
            playerRef.stats.weaponFireScreenShakeIntensity, playerRef.stats.weaponFireScreenShakeTime
            );
            animator.SetTrigger("PlayerFired");
            InstantiateNewBullet();
            SetVelocityAndDir(velocity);
        }
    }
    private void ResetCanAttackBool()
    {
        canShoot = true;
    }

    private void InstantiateNewBullet() // Change to object pool
    {
        bulletSound.Play();
        newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    }
    
    private void SetVelocityAndDir(float velocity) // Set the velocity and direction of the bullet depending who shot it
    {
        Vector2 direction;
        if (newTarget != null) // Use for boss
        {
            Vector3 tartgetPos = newTarget.position - newBullet.transform.position;
            direction = new Vector2(tartgetPos.x, tartgetPos.y);
        } 
        else 
        {
            direction = transform.right;
            //Debug.Log($"Player is shooting right: {direction}");
        }
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * velocity;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (newBullet != null )
        Gizmos.DrawLine(spawnPoint.position, newBullet.transform.position);
    }
}
