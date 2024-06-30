using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDebug : MonoBehaviour
{
    BossStatsScriptableObject stats;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawWireCube (transform.position, new Vector3 (
            transform.position.x + stats.bossFOV_BT, (transform.position.y + stats.bossFOV_BT) /3, 0));
    }
}
