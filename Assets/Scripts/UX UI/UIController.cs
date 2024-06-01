using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, endGameMenu;
    [SerializeField] GameManager gameManager;
    private bool showPauseMenu, showEndGameMenu;
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
        ResetAllMenus();
    }

    private void DisplayPauseMenu()
    {
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
        Debug.Log("Pause game event invoked");
    }

    public IEnumerator DisplayEndGameMenu(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        showEndGameMenu = !showEndGameMenu;
        endGameMenu.SetActive(showEndGameMenu);
    }

    public void ClickRestartGame()
    {
        StartCoroutine(gameManager.LoadSceneCoroutine(0.1f, 0));
        Debug.Log("player clicked restart game");
    }

    public void ClickQuitGame()
    {
        Application.Quit();
        Debug.Log("player clicked restart game");
    }

    private void ResetAllMenus()
    {
        showPauseMenu = false;
        pauseMenu.SetActive(false);
        showEndGameMenu = false;
        endGameMenu.SetActive(false);
    }


    
}
