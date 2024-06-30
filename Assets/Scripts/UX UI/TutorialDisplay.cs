using System.Collections;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("Add the tutorial pannel gameobject here.")] 
    private GameObject tutorial;
    [SerializeField, Tooltip("Add each tutorial display in order here.")] 
    private GameObject[] tutorialDispaly;
    [SerializeField, Tooltip("Set how much time there is between each tutorial display.")] 
    private float timeBetweenTutorialDispaly;

    private void Start()
    {
        StartCoroutine(GoThroughTutorialInput());
    }
    private IEnumerator GoThroughTutorialInput()
    {
        for (int i = 0; i < tutorialDispaly.Length; ++i)
        {
            tutorialDispaly[i].SetActive(true);
            yield return new WaitForSeconds(timeBetweenTutorialDispaly);
            tutorialDispaly[i].SetActive(false);
        }
        tutorial.SetActive(false);
    }
}
