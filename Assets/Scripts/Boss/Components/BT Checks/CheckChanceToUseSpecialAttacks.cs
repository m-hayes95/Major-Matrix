using UnityEngine;
using BehaviourTree;

public class CheckChanceToUseSpecialAttacks : BTNode
{
    private RandomChance randomChance;
    private float randomChancePercentIncrease;
    private float specialAttackPercentThreshold;
    private float randomNumber;
    public CheckChanceToUseSpecialAttacks(RandomChance randomChance, float percentIncrease, float specialAttackPercentThreshold)
    {
        this.randomChance = randomChance;
        randomChancePercentIncrease = percentIncrease;
        this.specialAttackPercentThreshold = specialAttackPercentThreshold;
    }
    public override NodeState Evaluate()
    {
        randomChance.ApplyRandomChanceOutOf100Percent(randomChancePercentIncrease);
        randomNumber = randomChance.GetRandomNumber();
        
        if (randomNumber <= specialAttackPercentThreshold)
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Chance Use Special Attack state = {state}");
            //Debug.Log($"Check Chance Use Special Attack state = {state}, the rand value = {randomNumber} <= {specialAttackPercentThreshold} ");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Check Chance Use Special Attack state = {state}");
        //Debug.Log($"Check Chance Use Special Attack state = {state}, the rand value = {randomNumber} > {specialAttackPercentThreshold}");
        return state;
    }
}
