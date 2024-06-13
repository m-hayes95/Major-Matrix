using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]private bool facingLeft = true;
    private enum StateMachine  { left, right }
    [SerializeField]private StateMachine m_Machine;
    /*
    public void LookAt(Transform thisTransform, Transform targetTransform)
    {   
        //float xDistanceFromTarget = thisTransform.position.x - targetTransform.position.x;
        //if (thisTransform.position.x < targetTransform.position.x)
        Debug.Log($"Target {targetTransform.position.x} ");
        if (thisTransform.position.x > targetTransform.position.x)
        {
            facingLeft = true;
        }
        if (thisTransform.position.x < targetTransform.position.x)
        {
            facingLeft = false;
        }
        if (facingLeft) m_Machine = StateMachine.left;
        else m_Machine = StateMachine.right;
        switch (m_Machine)
        {
            case StateMachine.left:
                
                transform.Rotate(0f, -180f, 0f);
                break;
            case StateMachine.right:
                transform.Rotate(0f, 0f, 0f);
                break;
        }
        

    }
    */
    public void LookAt(Transform thisTransform, Transform targetTransform)
    {
        // Check which side the player is on
        float xDistanceFromTarget = thisTransform.position.x - targetTransform.transform.position.x;
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
