using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Shield : MonoBehaviour
{
    //Refs
    private BossStatsScriptableObject stats;
    [SerializeField] private GameObject shieldVisual;
    // Check if shield is up
    private bool isShielded = false;
    private bool canShield = true;
    private int currentShields;
    private BossType bossType;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        bossType = GetComponent<BossType>();
    }

    private void Start()
    {
        currentShields = stats.maxShields;
    }

    public bool GetShieldStatus()  {  return isShielded; } 
    public bool GetCanShield()  {  return canShield; } 
    public int GetCurrentShieldsAmount()  {  return currentShields; } 

    public void UseShield()
    {
        // Make the boss resistant to player damage
        if (currentShields > 0 && canShield)
        {
            if (bossType.CheckIfBossHasBT()) SavedStats.Instance.StoreTimesUsedShieldBT();
            else SavedStats.Instance.StoreTimesUsedShieldSM();
            canShield = false;
            isShielded = true;
            currentShields--;
            shieldVisual.SetActive(true);
            StartCoroutine(ShieldTimer(stats.shieldTimer));
        }
    }
    private IEnumerator ShieldTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Debug.Log("Shield reset");
        isShielded = !isShielded;
        shieldVisual.SetActive(false);
        StartCoroutine(ShieldCooldownTimer());
    }
    private IEnumerator ShieldCooldownTimer()
    {
        int randomTime = Random.Range(1, 8); // Change later
        yield return new WaitForSeconds(randomTime);
        canShield = true;
    }
}
