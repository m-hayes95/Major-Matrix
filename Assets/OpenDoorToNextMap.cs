using UnityEngine;

public class OpenDoorToNextMap : MonoBehaviour
{
    // Subscribes to the boss health unity event, on Boss dead - then opens the door to the next level
    public void OpenDoor()
    {
        gameObject.SetActive(true);
    }
}
