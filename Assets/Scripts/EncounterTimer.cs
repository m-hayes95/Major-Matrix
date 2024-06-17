using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTimer : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private EndGameMenuStats endGameMenuStats;
    private float encounterTimer;

    public float GetEncounterTimer() { return encounterTimer; }
    private void Update()
    {
        if (playerHealth != null && bossHealth != null)
        {
            IncreaseTime();
            SaveEncounterTime();
        }
    }
    private void IncreaseTime()
    {
        if (!playerHealth.GetIsDead() || !bossHealth.GetIsDead())
            encounterTimer += Time.deltaTime;
    }
    private void SaveEncounterTime()
    {
        if (playerHealth.GetIsDead() || bossHealth.GetIsDead())
        {
            endGameMenuStats.SaveEncounterTimer(encounterTimer);
        }
    }
}
