using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void PauseGameAction();
    public static event PauseGameAction OnPaused;

    private float encounterTimer;

    private bool isGamePaused = false;


    private void Update()
    {
        encounterTimer += Time.deltaTime;
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

    public bool GetIsGamePaused() { return isGamePaused; }
    public float GetEncounterTimer() {  return encounterTimer; }
}
