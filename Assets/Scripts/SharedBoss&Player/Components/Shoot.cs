using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField, Tooltip("Add the bullet prefab to this field.")] 
    private GameObject bullet;
    [SerializeField, Tooltip("Add the spawn point transform, where the bullet will spawn from, to this field")] 
    private Transform spawnPoint;
    [SerializeField] private AudioSource bulletSound;
    private Transform newTarget;
    private GameObject newBullet;

    public void FireWeaponBoss(Transform target, float velocity)
    {
        InstantiateNewBullet();
        newTarget = target;
        SetVelocityAndDir(velocity);
    }
    public void FireWeaponPlayer(float velocity)
    {
        InstantiateNewBullet();
        SetVelocityAndDir(velocity);
    }

    private void InstantiateNewBullet()
    {
        bulletSound.Play();
        newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    }
    
    private void SetVelocityAndDir(float velocity)
    {
        Vector2 direction;
        if (newTarget != null) // Use for boss
        {
            Vector3 tartgetPos = newTarget.position - newBullet.transform.position;
            direction = new Vector2(tartgetPos.x, tartgetPos.y);
        } 
        else 
        {
            direction = transform.right;
            Debug.Log($"Player is shooting right: {direction}");
        }
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * velocity;

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (newBullet != null )
        Gizmos.DrawLine(spawnPoint.position, newBullet.transform.position);
    }
}
