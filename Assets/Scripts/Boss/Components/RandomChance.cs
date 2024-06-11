using UnityEngine;

public class RandomChance : MonoBehaviour
{
    // Get a random chance out of 100 and increase by percentage for more control over outcome occuring
    public float ApplyRandomChanceOutOf100Percent(float chancePercentageIncrease) 
    {
        float random = Random.Range(0f, 101f);
        float percentageModifier = (100 + chancePercentageIncrease) / 100;
        
        float newNumber = random * percentageModifier;
        return newNumber;
    }
}
