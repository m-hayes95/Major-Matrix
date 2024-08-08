using UnityEngine;

public class ChangeTimeScale : MonoBehaviour
{
    private bool isGamePaused;

    private void OnEnable() { GameManager.OnPaused += ToggleIsGamePaused; }
    private void OnDisable() { GameManager.OnPaused -= ToggleIsGamePaused; }

    private void Update()
    {
        if (isGamePaused) Time.timeScale = 0;
        if (!isGamePaused) Time.timeScale = 1;  
    }
    // Set pause status changing time scale from 0 to 1 (Need to refactor, so we are not using timescale)
    private void ToggleIsGamePaused() { isGamePaused = !isGamePaused; }
    
}
