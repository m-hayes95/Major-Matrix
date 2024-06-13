using System.Runtime.CompilerServices;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    private bool facingLeft = true;
    private void Look()
    {   
        Debug.Log("hjwjfij");
        float xDistanceFromTarget = transform.position.x - targetTransform.position.x;
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
        Debug.Log("Boss Flipped");
    }
}
