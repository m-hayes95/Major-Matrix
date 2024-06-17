using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextMap : MonoBehaviour
{
    [SerializeField, Tooltip("Choose the next scene (by index) that will be loaded when the player enters the door ")] 
    private int nextScene;
    [SerializeField,Tooltip("Set the game object transform for the mid visuals of the door - these will spin on update")]
    private Transform visualsTransform;
    private SpinDoorVisuals spinDoorVisuals;

    private void Awake()
    {
        spinDoorVisuals = GetComponent<SpinDoorVisuals>();
    }
    private void Update()
    {
        spinDoorVisuals.Spin(visualsTransform); // Spin the center of the door for visual effects
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) // When the other game object is a Player, move to the next map
            NextMap();
    }
    private void NextMap()
    {
        SceneManager.LoadScene(nextScene);
    }
}
