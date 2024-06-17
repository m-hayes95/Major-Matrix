using UnityEngine;

public class SpinDoorVisuals : MonoBehaviour
{ // Rotate the door viusals for visual effects
    public void Spin(Transform transform)
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }
}
