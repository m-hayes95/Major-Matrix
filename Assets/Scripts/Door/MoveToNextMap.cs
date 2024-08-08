using UnityEngine;

public class MoveToNextMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<NewPlayerController>()) // When the other game object is a Player, move to the next map
            NextMap();
    }
    private void NextMap() // Load the next map using the load order sequence
    {
        SceneLoadOrder.Instance.LoadNextScene();
    }
}
