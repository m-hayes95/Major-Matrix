using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadOrder : MonoBehaviour
{
    // Signleton class used to store data on the scene load order and what scene we are currently on
    // This is used with the MoveToNextMap class to randomise the boss fight order
    public static SceneLoadOrder Instance { get; private set; }
    private int[] sceneIndexOrder;
    private int bossFight_BT = 1;
    private int safeRoom = 2;
    private int bossFight_SM = 3;
    private int endScene = 4;
    private int sceneCount = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);    
        }
    }

    private void Start()
    {   
        // Set boss encounter order to random
        sceneIndexOrder = new int[4];
        if (RandomChance())
            InitialiseLoadOrder_BT_First();
        else
            InitialiseLoadOrder_SM_First();
    }
    private void InitialiseLoadOrder_BT_First()
    {
        sceneIndexOrder[0] = bossFight_BT;
        sceneIndexOrder[1] = safeRoom;
        sceneIndexOrder[2] = bossFight_SM;
        sceneIndexOrder[3] = endScene;
    }

    private void InitialiseLoadOrder_SM_First()
    {
        sceneIndexOrder[0] = bossFight_SM;
        sceneIndexOrder[1] = safeRoom;
        sceneIndexOrder[2] = bossFight_BT;
        sceneIndexOrder[3] = endScene;
    }

    private bool RandomChance()
    {
        if (Random.Range(0,2) == 0)
            return true;
        return false;
    }

    public void LoadNextScene()
    {
        int nextScene = sceneIndexOrder[sceneCount];
        SceneManager.LoadScene(nextScene);
        sceneCount++;
    }
}
