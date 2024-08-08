using UnityEngine;

public class BossStatsComponent : MonoBehaviour
{
    // This class acts as a reference for the Bosses Stats to use in the Boss AI script
    [Tooltip("Place the scriptable object for the boss stats here")] 
    public BossStatsScriptableObject bossStats;
}
