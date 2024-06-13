using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public void Chase(Transform target, Transform currentLocation, float moveSpeed)
    {
        MoveToPlayer(target, currentLocation, moveSpeed);
    }
    private void MoveToPlayer(Transform target, Transform currentLocation, float moveSpeed)
    {
        Vector2 targetXPos = new Vector2(target.transform.position.x, currentLocation.position.y);
        currentLocation.position =
            Vector2.MoveTowards(currentLocation.position, targetXPos, moveSpeed * Time.deltaTime);
    }
}
