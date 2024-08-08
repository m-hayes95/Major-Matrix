using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private BossType bossType;
    private void Awake()
    {
        bossType = GetComponent<BossType>();
    }
    public void Chase(Transform target, Transform currentLocation, float moveSpeed) // Called from FSM or BT
    {
        MoveToPlayer(target, currentLocation, moveSpeed);
        // Save time spent chasing stats
        if (bossType.CheckIfBossHasBT()) SavedStats.Instance.StoreTimeSpentUsingChaseBT();
        else SavedStats.Instance.StoreTimeSpentUsingChaseSM();
    }
    private void MoveToPlayer(Transform target, Transform currentLocation, float moveSpeed) // Chase the player target
    {
        Vector2 targetXPos = new Vector2(target.transform.position.x, currentLocation.position.y);
        currentLocation.position =
            Vector2.MoveTowards(currentLocation.position, targetXPos, moveSpeed * Time.deltaTime);
    }
}
