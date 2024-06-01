using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void PauseGameAction();
    public static event PauseGameAction OnPaused;

    private bool isGamePaused = false;

    public void PauseGame() 
    { 
        isGamePaused = !isGamePaused;
        OnPaused?.Invoke();
    }

    public bool GetIsGamePaused() { return isGamePaused; }

}
