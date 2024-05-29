using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Refs
    private BossStatsScriptableObject stats;
    [SerializeField] private GameObject shieldVisual;
    // Check if shield is up
    private bool isShielded = false;
    private int currentShields;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
    }

    private void Start()
    {
        currentShields = stats.maxShields;
    }

    private void Update()
    {
        Debug.Log($"Current shields left: {currentShields}");
    }

    public bool GetShieldStatus()  {  return isShielded; } 
    public int GetCurrentShieldsAmount()  {  return currentShields; } 

    public void UseShield()
    {
        // Make the boss resistant to player damage
        if (currentShields > 0)
        {
            isShielded = true;
            currentShields--;
            shieldVisual.SetActive(true);
            StartCoroutine(ShieldTimer(stats.shieldTimer));
        }
    }
    private IEnumerator ShieldTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Shield reset");
        isShielded = !isShielded;
        shieldVisual.SetActive(false);
    }
}
