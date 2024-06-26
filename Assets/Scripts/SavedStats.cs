using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStats : MonoBehaviour
{
    // Signleton used to store times for each boss encounter, which will be displayed at the end of the game
    public static SavedStats Instance { get; private set; }
    [SerializeField]private float savedEncouterTimeBT;
    [SerializeField]private float savedEncouterTimeSM;
    [SerializeField]private float playerDeathCountBT;
    [SerializeField]private float playerDeathCountSM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
    }
    public void StoreCurrentTimeBT(float currentTimeBT)
    {
        savedEncouterTimeBT = currentTimeBT;
    }
    public void StoreCurrentTimeSM(float currentTimeSM)
    {
        savedEncouterTimeSM = currentTimeSM;
    }
    // Add to Player On death listener depending on which boss the player is facing
    // adds to the total deaths for each enconter
    public void StoreCurrentPlayerDeathsSM()
    {
        playerDeathCountSM++;
    }
    public void StoreCurrentPlayerDeathsBT()
    {
        playerDeathCountBT++;
    }

    public float GetEncounterTimeForBT() { return savedEncouterTimeBT; }
    public float GetEncounterTimeForSM() { return savedEncouterTimeSM; }
    public float GetCurrentPlayerDeathCountForSM() { return playerDeathCountSM; }
    public float GetCurrentPlayerDeathCountForBT() { return playerDeathCountBT; }
}
