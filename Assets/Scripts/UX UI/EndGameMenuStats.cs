using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EndGameMenuStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathCount_SM, timeCount_SM, damageDealtCount_SM;
    [SerializeField] private TextMeshProUGUI deathCount_BT, timeCount_BT, damageDealtCount_BT;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject quitButton, statsDisplay;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private TextMeshProUGUI shotCount_SM, meleeCount_SM, specialCount_SM, shieldCount_SM, timeSpentChasingCount_SM;
    [SerializeField] private TextMeshProUGUI shotCount_BT, meleeCount_BT, specialCount_BT, shieldCount_BT, timeSpentChasingCount_BT;

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
        DisplayDamageDealt();
        DisplayShotCount();
        DisplayMeleeCount();
        DisplaySpecialCount();
        DisplayShieldCount();
        DisplayTimeSpentChasing();
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
    private void DisplayDamageDealt()
    {
        damageDealtCount_SM.text = SavedStats.Instance.GetCurrentDamageDealtSM().ToString();
        damageDealtCount_BT.text = SavedStats.Instance.GetCurrentDamageDealtBT().ToString();
    }

    private void DisplayShotCount()
    {
        shotCount_SM.text = SavedStats.Instance.GetTimesUsedShootSM().ToString();
        shotCount_BT.text = SavedStats.Instance.GetTimesUsedShootBT().ToString();
    }
    private void DisplayMeleeCount()
    {
        meleeCount_SM.text = SavedStats.Instance.GetTimesUsedMeleeSM().ToString();
        meleeCount_BT.text = SavedStats.Instance.GetTimesUsedMeleeBT().ToString();
    }
    private void DisplaySpecialCount()
    {
        specialCount_SM.text = SavedStats.Instance.GetTimesUsedSpecialAttackSM().ToString();
        specialCount_BT.text = SavedStats.Instance.GetTimesUsedSpecialAttackBT().ToString();
    }
    private void DisplayShieldCount()
    {
        shieldCount_SM.text = SavedStats.Instance.GetTimesUsedShieldSM().ToString();
        shieldCount_BT.text = SavedStats.Instance.GetTimesUsedShieldBT().ToString();
    
    }

    private void DisplayTimeSpentChasing()
    {
        timeSpentChasingCount_SM.text = SavedStats.Instance.GetTimeSpentUsingChaseSM().ToString("00:00");
        timeSpentChasingCount_BT.text = SavedStats.Instance.GetTimeSpentUsingChaseBT().ToString("00:00");
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
