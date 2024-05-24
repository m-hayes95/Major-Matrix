using UnityEngine;

public abstract class BossAI : MonoBehaviour
{
    //Boss stats - add sciptable object
    [Header("Boss Stats"), SerializeField] protected float moveSpeed;
    [SerializeField] protected float normalAttackDamage;
    [SerializeField] protected float specialAttackDamage;
    [SerializeField] protected float hP;
    // Thresholds
    [SerializeField] protected float closeRangeAttackThreshold;
    [SerializeField] protected float longRangeAttackThreshold;
    // Components
    [SerializeField] protected GameObject player;
    protected VectorDistanceChecker vectorDistanceChecker;
    // Distance from player
    protected float distanceFromPlayer;

    private void Awake()
    {
        vectorDistanceChecker = GetComponent<VectorDistanceChecker>();
    }

    protected virtual void Update()
    {
        distanceFromPlayer = 
            vectorDistanceChecker.CheckVector2DistanceBetweenAandB(gameObject, player);
        Debug.Log($" The boss is { distanceFromPlayer} from the player");
    }
    private void FacePlayer()
    {
        // Look towards the player
    }
    protected void Move()
    {
       // Move the boss character
    }

    protected void NormalRangeAttack()
    {
        // Attack the player if they cross a certain distance
    }
    protected void NormalCloseAttack()
    {
        // Attack the player if they get too close
    }

    protected void SpecialLowAttack()
    {
        // Cross Screen attack from the ground
    }
    protected void SpecialHighAttack()
    {
        // Cross screen attack from the ceiling
    }
    protected void Sheild()
    {

    }

}
