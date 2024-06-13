using System.Collections;
using UnityEngine;

public class RandomChance : MonoBehaviour
{
    private float randomNumber = 0;
    private bool generateNew = true;
    // Get a random chance out of 100 and increase by percentage for more control over outcome occuring
    // (generate a new number each second when called multiple times)

    public float GetRandomNumber() { return randomNumber; }
    public void ApplyRandomChanceOutOf100Percent(float chancePercentageIncrease) 
    {
        if (generateNew)
        {
            generateNew = false;
            float random = Random.Range(0f, 101f);
            float percentageModifier = (100 + chancePercentageIncrease) / 100;
            randomNumber = random * percentageModifier;
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        generateNew = true;
    }
}
