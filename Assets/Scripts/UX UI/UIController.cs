using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Main Menu Refs
    [SerializeField, Tooltip("Add reference for the pause menu to this field")]
    private GameObject pauseMenu;
    [SerializeField, Tooltip("Add reference for the game over menu to this field")]
    private GameObject endGameMenu;
    [SerializeField, Tooltip("Add reference for the pause sub menus to these fields")] 
    private GameObject settingsTab, controlsTab, creditsTab;
    [SerializeField, Tooltip("Add reference for the game manager to this field")] 
    private GameManager gameManager;

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
    public void DisplayPauseMenu() // On pause game
    {
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
        Debug.Log("Pause game event invoked");
    }
    public void DisplayGameOverMenu() // When player dies
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
        StartCoroutine(gameManager.ReloadCurrentScene(0.1f));
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
    public void SetIsTabsOpen(bool b) { isTabsOpen = b; } // Allow other scripts to force set if the tab is open
    public bool GetIsTabsOpen() { return isTabsOpen; } // Let other scripts check if tabs are open 
    
}
