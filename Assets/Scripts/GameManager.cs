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
}
