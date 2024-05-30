using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint, targetPoint;
    public float velocity;
    private GameObject track;
    private void Update()
    {
        transform.right = targetPoint.position - transform.position;

        if (Input.GetMouseButtonDown(0)) {
            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            track = newBullet;
            //newBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right * force, ForceMode2D.Impulse);
            newBullet.GetComponent<Rigidbody2D>().velocity = 
                transform.TransformDirection(Vector2.right * velocity);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        if (track  != null)
        Gizmos.DrawLine(spawnPoint.position, track.transform.position);
    }
}
