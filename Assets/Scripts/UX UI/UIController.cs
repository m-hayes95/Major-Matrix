using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool showPauseMenu;
    private void OnEnable()
    {
        GameManager.OnPaused += DisplayPauseMenu;
    }
    private void OnDisable()
    {
        GameManager.OnPaused -= DisplayPauseMenu;
    }
    private void Start()
    {
        showPauseMenu = false;
        pauseMenu.SetActive(false);
    }

    private void DisplayPauseMenu()
    {
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
        Debug.Log("Pause game event invoked");
    }


    
}
