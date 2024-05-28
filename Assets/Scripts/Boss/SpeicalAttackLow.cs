using UnityEngine;

public class SpeicalAttackLow : BossAI
{
    [SerializeField] GameObject specialAttackGameObject;
    private Vector3 leftPosition, rightPosition;
    

    private void Start()
    {
        leftPosition = transform.position + Vector3.down * stats.offsetY;
        rightPosition = transform.position + Vector3.down * stats.offsetY;
    }
    /*
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExecuteSpecialAttack();
        }
    }
    */
    public void ExecuteSpecialAttack()
    {
        for (int i = 0; i < stats.numberOfAttacks; ++i)
        {
            SpawnLeft(-stats.spacingX);
            SpawnRight(stats.spacingX);
        }
    }

    private void SpawnLeft(float spacing)
    {
        leftPosition += new Vector3(spacing, stats.offsetY, 0);
        GameObject newAttack = Instantiate(
            specialAttackGameObject, leftPosition, Quaternion.identity
            );
        
    }

    private void SpawnRight(float spacing)
    {
        rightPosition += new Vector3(spacing, stats.offsetY, 0);
        GameObject newAttack = Instantiate(
            specialAttackGameObject, rightPosition, Quaternion.identity
            );
    }
}
