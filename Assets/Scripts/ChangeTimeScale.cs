using System.Collections;
using System.Collections.Generic;
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

    private void ToggleIsGamePaused() { isGamePaused = !isGamePaused; }
    
}
