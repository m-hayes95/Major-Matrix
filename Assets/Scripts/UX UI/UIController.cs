using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, endGameMenu;
    [SerializeField] GameObject settingsTab, controlsTab, creditsTab;
    [SerializeField] GameManager gameManager;
    private bool showPauseMenu, showEndGameMenu;
    private bool isTabsOpen;
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
    public void DisplayPauseMenu()
    {
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
        Debug.Log("Pause game event invoked");
    }
    public void DisplayGameOverMenu()
    {
        StartCoroutine(ShowGameOverMenu());
    }
    private IEnumerator ShowGameOverMenu()
    {
        yield return new WaitForSeconds(2f);
        showEndGameMenu = !showEndGameMenu;
        endGameMenu.SetActive(showEndGameMenu);
    }

    public void ClickRestartGame()
    {
        StartCoroutine(gameManager.LoadSceneCoroutine(0.1f));
        Debug.Log("player clicked restart game");
    }

    public void ClickQuitGame()
    {
        Application.Quit();
        Debug.Log("player clicked restart game");
    }

    private void ResetAllMenus()
    {
        // Close Menus
        showPauseMenu = false;
        pauseMenu.SetActive(false);
        showEndGameMenu = false;
        endGameMenu.SetActive(false);
        // Close Tabs
        settingsTab.SetActive(false);
        controlsTab.SetActive(false);
        creditsTab.SetActive(false);
    }

    public void CloseOpenTabs()
    {
        isTabsOpen = false; 
        settingsTab.SetActive(false);
        controlsTab.SetActive(false);
        creditsTab.SetActive(false);
    }
    public void SetIsTabsOpen(bool b) { isTabsOpen = b; }
    public bool GetIsTabsOpen() { return isTabsOpen; }
    
}
