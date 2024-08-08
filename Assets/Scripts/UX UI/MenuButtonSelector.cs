using UnityEngine;
using UnityEngine.EventSystems;
// For controllers support, this script selects the first option when a new menu is opened
public class MenuButtonSelector : MonoBehaviour
{
    // Select Button First
    [SerializeField, Tooltip("Choose the first button for the settings menu")] 
    private GameObject settingsMenuFirstButton;
    [SerializeField, Tooltip("Choose the first button for the controls menu")] 
    private GameObject controlsMenuFirstButton;
    [SerializeField, Tooltip("Choose the first button for the credits menu")] 
    private GameObject creditsMenuFirstButton;
    [SerializeField, Tooltip("Choose the first button for the pause menu")] 
    private GameObject pauseMenuFirstButton;
    [SerializeField, Tooltip("Choose the first button for the game over menu")] 
    private GameObject gameOverMenuFirstButton;
    // Menus
    [SerializeField, Tooltip("Add a reference to the pause menu to this field")] 
    private GameObject pauseMenu;
    [SerializeField, Tooltip("Add a reference to the game over menu to this field")] 
    private GameObject gameOverMenu;
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
