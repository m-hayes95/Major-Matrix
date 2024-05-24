using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDistanceChecker : MonoBehaviour
{
    public float CheckVector2DistanceBetweenAandB(GameObject pointA, GameObject pointB)
    {
        float distance;
        distance = Vector2.Distance(pointA.transform.position, pointB.transform.position);
        return distance;
    }
}
