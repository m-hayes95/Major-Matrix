using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField, Tooltip("Add the bullet prefab to this field.")] 
    private GameObject bullet;
    [SerializeField, Tooltip("Add the spawn point transform, where the bullet will spawn from, to this field")] 
    private Transform spawnPoint;

    /* Test
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon(Vector3.right, 100);
        }
    }
    */
    public void FireWeapon(Vector3 shootDirection, float shotForce)
    {
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootDirection * shotForce, ForceMode2D.Impulse);
    }
}
