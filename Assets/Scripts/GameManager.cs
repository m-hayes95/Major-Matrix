using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void PauseGameAction();
    public static event PauseGameAction OnPaused;
    
    [SerializeField] private bool isGamePaused = false;
    
    public void PauseGame() 
    { 
        isGamePaused = !isGamePaused;
        OnPaused?.Invoke();
    }

    public IEnumerator LoadSceneCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ManuallySetIsGamePaused()
    {
        // Used with UI events on resume game click
        isGamePaused = !isGamePaused;
    }
    public void StorePlayerDeathCountBTEncounter()
    {
        SavedStats.Instance.StoreCurrentPlayerDeathsBT();
    }
    public void StorePlayerDeathCountSMEncounter()
    {
        SavedStats.Instance.StoreCurrentPlayerDeathsSM();
    }

    public bool GetIsGamePaused() { return isGamePaused; }
}
