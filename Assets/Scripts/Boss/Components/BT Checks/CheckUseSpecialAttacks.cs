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
        float rand = randomChance.ApplyRandomChanceOutOf100Percent(
            randomChancePercentIncrease);
        if (rand <= specialAttackPercentThreshold)
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Use Special Attack state = {state}, the rand value = {rand} ");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Check Use Special Attack state = {state}, the rand value = {rand} ");
        return state;
    }
}
