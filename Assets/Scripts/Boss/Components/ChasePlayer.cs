using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private bool facingLeft = true;

    public void Chase(Transform target, Transform currentLocation, float moveSpeed)
    {
        MoveToPlayer(target, currentLocation, moveSpeed);
        CheckWhichSidePlayerIsOn(target, currentLocation);
    }
    private void MoveToPlayer(Transform target, Transform currentLocation, float moveSpeed)
    {
        Vector2 targetXPos = new Vector2(target.transform.position.x, currentLocation.position.y);
        currentLocation.position =
            Vector2.MoveTowards(currentLocation.position, targetXPos, moveSpeed * Time.deltaTime);
    }
    private void CheckWhichSidePlayerIsOn(Transform target, Transform currentLocation)
    {
        float xDistanceFromTarget = currentLocation.position.x - target.position.x;
        if (!facingLeft && xDistanceFromTarget > 0)
        {
            FacePlayer();
        }
        if (facingLeft && xDistanceFromTarget < 0)
        {
            FacePlayer();
        }
    }
    private void FacePlayer()
    {
        // Look towards the player depending on X pos
        facingLeft = !facingLeft;
        transform.Rotate(0f, -180f, 0f);
    }
}
