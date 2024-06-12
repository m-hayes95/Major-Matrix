using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToTargetY : MonoBehaviour
{
    public float GetDistanceY(Transform targetTransform, Transform thisTransform)
    {
        // Check distance between Y pos for boss and player, boss always 0
        float distanceY = targetTransform.position.y - thisTransform.position.y;
        //Debug.Log($"Y distance from player {distanceY}");
        return distanceY;
    }
}
