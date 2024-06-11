using UnityEngine;
using BehaviourTree;

public class CheckUseSpecialAttacks : BTNode
{
    private RandomChance randomChance;
    private float randomChancePercentIncrease;
    private float specialAttackPercentThreshold;
    public CheckUseSpecialAttacks(RandomChance randomChance, float percentIncrease, float specialAttackPercentThreshold)
    {
        this.randomChance = randomChance;
        randomChancePercentIncrease = percentIncrease;
        this.specialAttackPercentThreshold = specialAttackPercentThreshold;
    }
    public override NodeState Evaluate()
    {
        if (randomChance.ApplyRandomChanceOutOf100Percent(
            randomChancePercentIncrease) > specialAttackPercentThreshold
            )
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Use Special Attack state = {state}");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Check Use Special Attack state = {state}");
        return state;
    }
}
