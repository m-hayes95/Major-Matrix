using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextMap : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private SpinDoorVisuals spinDoorVisuals;
    [SerializeField] private Transform visualsTransform;

    private void Awake()
    {
        spinDoorVisuals = GetComponent<SpinDoorVisuals>();
    }
    private void Update()
    {
        spinDoorVisuals.Spin(visualsTransform);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
            NextMap();
    }
    private void NextMap()
    {
        SceneManager.LoadScene(nextScene);
    }
}
