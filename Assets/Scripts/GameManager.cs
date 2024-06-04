using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void PauseGameAction();
    public static event PauseGameAction OnPaused;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private EndGameMenuStats endGameMenuStats;
    private float encounterTimer;
    [SerializeField] private bool isGamePaused = false;

    private void Update()
    {
        if (!playerHealth.GetIsDead() || !bossHealth.GetIsDead())
        if (!isGamePaused) encounterTimer += Time.deltaTime;
        
        if (playerHealth.GetIsDead() || bossHealth.GetIsDead())
        {
            endGameMenuStats.SaveEncounterTimer(encounterTimer);
        }
    }

    public void PauseGame() 
    { 
        isGamePaused = !isGamePaused;
        OnPaused?.Invoke();
    }

    public IEnumerator LoadSceneCoroutine(float seconds, int sceneIndex)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneIndex);
    }
    public void ManuallySetIsGamePaused()
    {
        // Used with UI events on resume game click
        isGamePaused = !isGamePaused;
    }
    public bool GetIsGamePaused() { return isGamePaused; }
    public float GetEncounterTimer() {  return encounterTimer; }
}
