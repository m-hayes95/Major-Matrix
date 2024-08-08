using UnityEngine;

public class SavedStats : MonoBehaviour
{
    // Signleton used to store times for each boss encounter, which will be displayed at the end of the game
    public static SavedStats Instance { get; private set; }
    // Behaviour Tree stats
    [SerializeField] private float savedEncouterTimeBT;
    [SerializeField] private float playerDeathCountBT;
    [SerializeField] private float damageDealtToPlayerBT;
    [SerializeField] private float timeSpentUsingChaseBT;
    [SerializeField] private int timesUsedShootBT;
    [SerializeField] private int timesUsedMeleeBT;
    [SerializeField] private int timesUsedSpecialAttackBT;
    [SerializeField] private int timesUsedShieldBT;
    // State Machine stats
    [SerializeField] private float savedEncouterTimeSM;
    [SerializeField] private float playerDeathCountSM;
    [SerializeField] private float damageDealtToPlayerSM;
    [SerializeField] private float timeSpentUsingChaseSM;
    [SerializeField] private int timesUsedShootSM;
    [SerializeField] private int timesUsedMeleeSM;
    [SerializeField] private int timesUsedSpecialAttackSM;
    [SerializeField] private int timesUsedShieldSM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
    }
    // Reset stats when the player dies on an encounter
    public void ResetBTCountStats()
    {
        timeSpentUsingChaseBT = 0;
        timesUsedShootBT = 0;
        timesUsedMeleeBT = 0;
        timesUsedSpecialAttackBT = 0;   
        timesUsedShieldBT = 0;
    }
    public void ResetSMCountStats()
    {
        timeSpentUsingChaseSM = 0;
        timesUsedShootSM = 0;
        timesUsedMeleeSM = 0;
        timesUsedSpecialAttackSM = 0;
        timesUsedShieldSM = 0;
    }
    // Behaviour Tree Setters
    public void StoreCurrentPlayerDeathsBT() => playerDeathCountBT++;
    public void StoreCurrentTimeBT(float currentTimeBT) => savedEncouterTimeBT = currentTimeBT;
    public void StoreCurrentDamageDealtBT(float damageDealt) => damageDealtToPlayerBT = damageDealt;
    public void StoreTimeSpentUsingChaseBT() => timeSpentUsingChaseBT += Time.deltaTime;
    public void StoreTimesUsedShootBT() => timesUsedShootBT++;
    public void StoreTimesUsedMeleeBT() => timesUsedMeleeBT++;
    public void StoreTimesUsedSpecialAttackBT() => timesUsedSpecialAttackBT++;
    public void StoreTimesUsedShieldBT() => timesUsedShieldBT++;

    // Behaviour Tree Getters
    public float GetEncounterTimeForBT() { return savedEncouterTimeBT; }
    public float GetCurrentPlayerDeathCountForBT() { return playerDeathCountBT; }
    public float GetCurrentDamageDealtBT() { return damageDealtToPlayerBT; }
    public float GetTimeSpentUsingChaseBT() { return timeSpentUsingChaseBT; }
    public float GetTimesUsedShootBT() { return timesUsedShootBT; }
    public float GetTimesUsedMeleeBT() { return timesUsedMeleeBT; }
    public float GetTimesUsedSpecialAttackBT() { return timesUsedSpecialAttackBT; }
    public float GetTimesUsedShieldBT() { return timesUsedShieldBT; }

    // Finate State Machine Setters
    public void StoreCurrentTimeSM(float currentTimeSM) => savedEncouterTimeSM = currentTimeSM;
    public void StoreCurrentPlayerDeathsSM() => playerDeathCountSM++;
    public void StoreCurrentDamageDealtSM(float damageDealt) => damageDealtToPlayerSM = damageDealt;
    public void StoreTimeSpentUsingChaseSM() => timeSpentUsingChaseSM += Time.deltaTime;
    public void StoreTimesUsedShootSM() => timesUsedShootSM++;
    public void StoreTimesUsedMeleeSM() => timesUsedMeleeSM++;
    public void StoreTimesUsedSpecialAttackSM() => timesUsedSpecialAttackSM++;
    public void StoreTimesUsedShieldSM() => timesUsedShieldSM++;

    // Finate State Machine Getters
    public float GetEncounterTimeForSM() { return savedEncouterTimeSM; }
    public float GetCurrentPlayerDeathCountForSM() { return playerDeathCountSM; }
    public float GetCurrentDamageDealtSM() { return damageDealtToPlayerSM; }
    public float GetTimeSpentUsingChaseSM() { return timeSpentUsingChaseSM; }
    public float GetTimesUsedShootSM() { return timesUsedShootSM; }
    public float GetTimesUsedMeleeSM() { return timesUsedMeleeSM; }
    public float GetTimesUsedSpecialAttackSM() { return timesUsedSpecialAttackSM; }
    public float GetTimesUsedShieldSM() { return timesUsedShieldSM; }
}
