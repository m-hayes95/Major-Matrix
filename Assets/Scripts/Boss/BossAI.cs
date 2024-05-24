using UnityEngine;

public abstract class BossAI : MonoBehaviour
{
    //Boss stats
    protected BossStatsScriptableObject stats;
    // Distance from player
    protected GameObject player;
    protected VectorDistanceChecker vectorDistance;
   
    protected float distanceFromPlayer;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        player = FindObjectOfType<PlayerController>().gameObject;
        vectorDistance = GetComponent<VectorDistanceChecker>();
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            distanceFromPlayer =
            vectorDistance.CheckVector2DistanceBetweenAandB(gameObject, player);
            Debug.Log($" The boss is {distanceFromPlayer} from the player");
            Debug.Log($" Attack target is {player.name}");
        }
        else
            Debug.LogWarning("Player object not found in BossAI script.");
        FacePlayer();
    }
    private void FacePlayer()
    {
        // Look towards the player depending on X pos
        if (player != null)
        {
            if (transform.position.x - player.transform.position.x < 0)
                transform.localScale = new Vector3(-2, 2, 2);
            else transform.localScale = new Vector3(2, 2, 2);
        }
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
