using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStats : MonoBehaviour
{
    // Signleton used to store times for each boss encounter, which will be displayed at the end of the game
    public static SavedStats Instance { get; private set; }
    [SerializeField]private float savedEncouterTimeBT;
    [SerializeField]private float savedEncouterTimeSM;
    [SerializeField]private float playerDeathCount;
    private void OnEnable()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(Instance);
        
    }
    public void StoreCurrentTimeBT(float currentTimeBT)
    {
        savedEncouterTimeBT = currentTimeBT;
    }
    public void StoreCurrentTimeSM(float currentTimeSM)
    {
        savedEncouterTimeSM = currentTimeSM;
    }

    public void StoreCurrentPlayerDeaths()
    {
        playerDeathCount++;
    }

    public float GetEncounterTimeForBT() { return savedEncouterTimeBT; }
    public float GetEncounterTimeForSM() { return savedEncouterTimeSM; }
    public float GetCurrentPlayerDeathCount() { return playerDeathCount; }
}
