using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EndGameMenuStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathCount_SM, timeCount_SM;
    [SerializeField] private TextMeshProUGUI deathCount_BT, timeCount_BT;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject quitButton, statsDisplay;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private PlayerInputActions inputActions;

    private void OnEnable() { inputActions.Player.Enable(); }
    private void OnDisable() {  inputActions.Player.Disable(); }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.CloseMenu.performed += CloseCreditsTab;
    }
    private void Update()
    {
        DisplayPlayerDeathCount();
        DisplayEncounterTime();
    }

    private void DisplayPlayerDeathCount()
    {
        deathCount_SM.text = SavedStats.Instance.GetCurrentPlayerDeathCountForSM().ToString();
        deathCount_BT.text = SavedStats.Instance.GetCurrentPlayerDeathCountForBT().ToString();
    }

    private void DisplayEncounterTime()
    {
        timeCount_SM.text = SavedStats.Instance.GetEncounterTimeForSM().ToString("00:00");
        timeCount_BT.text = SavedStats.Instance.GetEncounterTimeForBT().ToString("00:00");
    }
    private void CloseCreditsTab(InputAction.CallbackContext context)
    {
        if (context.performed && creditsMenu.activeInHierarchy)
        {
            creditsMenu.SetActive(false);
            quitButton.SetActive(true);
            statsDisplay.SetActive(true);
            SelectCreditsMenuButton();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void SelectCreditsMenuButton()
    {
        EventSystem.current.SetSelectedGameObject(creditsButton);
    }
}
