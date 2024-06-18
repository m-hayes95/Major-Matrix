using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenuStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathCount_SM, timeCount_SM;
    [SerializeField] private TextMeshProUGUI deathCount_BT, timeCount_BT;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject quitButton, statsDisplay;

    private void Update()
    {
        DisplayPlayerDeathCount();
        DisplayEncounterTime();
        if (creditsMenu.activeInHierarchy)
            CloseCreditsTab();
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
    private void CloseCreditsTab()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            creditsMenu.SetActive(false);
            quitButton.SetActive(true);
            statsDisplay.SetActive(true);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
