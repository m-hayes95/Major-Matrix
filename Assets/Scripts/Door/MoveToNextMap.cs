using UnityEngine;

public class MoveToNextMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) // When the other game object is a Player, move to the next map
            NextMap();
    }
    private void NextMap()
    {
        SceneLoadOrder.Instance.LoadNextScene();
    }
}
