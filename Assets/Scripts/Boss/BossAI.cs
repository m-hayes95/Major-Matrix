using UnityEngine;

public abstract class BossAI : MonoBehaviour
{
    //Boss stats
    protected BossStatsScriptableObject stats;
    // Distance from player
    protected GameObject player;
    protected VectorDistanceChecker vectorDistanceChecker;
   
    protected float distanceFromPlayer;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        player = FindObjectOfType<PlayerController>().gameObject;
        vectorDistanceChecker = GetComponent<VectorDistanceChecker>();
    }

    protected virtual void Update()
    {
        distanceFromPlayer = 
            vectorDistanceChecker.CheckVector2DistanceBetweenAandB(gameObject, player);
        Debug.Log($" The boss is { distanceFromPlayer} from the player");
        Debug.Log($" Attack target is {player.name}");
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
