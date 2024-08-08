using UnityEngine;

public class DistanceToTargetY : MonoBehaviour
{
    // Used to check if boss should use high or low special attacks
    public float GetDistanceY(Transform targetTransform, Transform thisTransform)
    {
        // Check distance between Y pos for boss and player, boss always 0
        float distanceY = targetTransform.position.y - thisTransform.position.y;
        //Debug.Log($"Y distance from player {distanceY}");
        return distanceY;
    }
}
