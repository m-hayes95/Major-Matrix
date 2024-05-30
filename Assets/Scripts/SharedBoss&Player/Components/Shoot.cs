using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField, Tooltip("Add the bullet prefab to this field.")] 
    private GameObject bullet;
    [SerializeField, Tooltip("Add the spawn point transform, where the bullet will spawn from, to this field")] 
    private Transform spawnPoint;
    private GameObject newBullet;

    /* Test
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon(Vector3.right, 100);
        }
    }
    */
    public void FireWeapon(Transform target, float velocity)
    {
        newBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        Vector3 direction = target.position - newBullet.transform.position;
        newBullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(direction.x, direction.y).normalized * velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (newBullet != null )
        Gizmos.DrawLine(spawnPoint.position, newBullet.transform.position);
    }
}
