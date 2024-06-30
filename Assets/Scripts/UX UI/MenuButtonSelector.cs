using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonSelector : MonoBehaviour
{
    // Select Button First
    [SerializeField] private GameObject settingsMenuFirstButton;
    [SerializeField] private GameObject controlsMenuFirstButton;
    [SerializeField] private GameObject creditsMenuFirstButton;
    [SerializeField] private GameObject pauseMenuFirstButton;
    [SerializeField] private GameObject gameOverMenuFirstButton;
    // Menus
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    // Do once 
    private bool selectPauseMenuOnce;
    private bool selectGameOverMenuOnce;
    private void Update()
    {
        CheckPauseMenuIsActive();
        CheckGameOverMenuIsActive();
    }
    private void CheckPauseMenuIsActive()
    {
        if (!pauseMenu.activeSelf) selectPauseMenuOnce = false;
        if (pauseMenu.activeSelf && !selectPauseMenuOnce)
        {
            selectPauseMenuOnce = true;
            SetFirstButtonPauseMenu();
        }
    }
    private void CheckGameOverMenuIsActive()
    {
        if (!gameOverMenu.activeSelf) selectGameOverMenuOnce = false;
        if (gameOverMenu.activeSelf && !selectGameOverMenuOnce)
        {
            selectGameOverMenuOnce = true;
            SetFirstButtonGameOverMenu();
        }
    }
    private void SetFirstButtonPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }
    private void SetFirstButtonGameOverMenu()
    {
        EventSystem.current.SetSelectedGameObject(gameOverMenuFirstButton);
    }
    public void SetFirstButtonSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(settingsMenuFirstButton);
    }
    public void SetFirstButtonControlsMenu()
    {
        EventSystem.current.SetSelectedGameObject(controlsMenuFirstButton);
    }
    public void SetFirstButtonCreditsMenu()
    {
        EventSystem.current.SetSelectedGameObject(creditsMenuFirstButton);
    }
}
