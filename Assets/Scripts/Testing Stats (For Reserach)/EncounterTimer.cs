using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterTimer : MonoBehaviour
{
    private float encounterTimer;
    private const string SCENE_BT_AI = "BossFight_BT_Scene";
    private const string SCENE_SM_AI = "BossFight_FSM_Scene";

    public float GetEncounterTimer() { return encounterTimer; }
    private void Update()
    {
        IncreaseTime();
    }
    private void IncreaseTime() // Time displayed in top middle of boss fight
    {
        encounterTimer += Time.deltaTime;
    }
    public void SaveEncounterTime()
    {
        // Check which scene we are in and save the correct encounter time
        // This method is called on the Boss OnDeath event through the inspector
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == SCENE_BT_AI)
            SavedStats.Instance.StoreCurrentTimeBT(encounterTimer);
        if (currentSceneName ==  SCENE_SM_AI)
            SavedStats.Instance.StoreCurrentTimeSM(encounterTimer);
    }

}
