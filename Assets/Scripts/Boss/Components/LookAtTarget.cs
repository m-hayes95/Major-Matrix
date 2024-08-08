using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField, Tooltip("Add a reference to the target transform pos in scene, here")] private Transform targetTransform;
    private bool facingLeft = true;
    // This class is used with the statemachine boss as a component only
    // When the state machine ai class calls this script in update, it does not work
    private void Update()
    {
        // Check which side the player is on
        float xDistanceFromTarget = transform.position.x - targetTransform.transform.position.x;
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
