using UnityEngine;

public class SpinDoorVisuals : MonoBehaviour
{
    public void Spin(Transform transform)
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }
}
