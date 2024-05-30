using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField, Tooltip("Add the bullet prefab to this field.")] 
    private GameObject bullet;
    [SerializeField, Tooltip("Add the spawn point transform, where the bullet will spawn from, to this field")] 
    private Transform spawnPoint;
    private Vector3 trackShot;

    /* Test
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon(Vector3.right, 100);
        }
    }
    */
    public void FireWeapon(Transform shootDirection, float velocity)
    {
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        trackShot = newBullet.transform.position;
        newBullet.GetComponent<Rigidbody2D>().velocity =
                transform.TransformDirection(Vector2.right * velocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(spawnPoint.position, trackShot);
    }
}
