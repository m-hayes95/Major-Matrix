using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : BossAI
{
    private enum StateMachine 
    { Idle, RangeAttack, CloseAttack, SpecialLowAttack, SpecialHighAttack, Sheild };
    [SerializeField] private StateMachine sM;
    private void Start()
    {
        sM = StateMachine.Idle;
    }

    protected override void Update()
    {
        base.Update();
        switch (sM)
        {
            case StateMachine.Idle:
                if (distanceFromPlayer > stats.longRangeAttackThreshold && canAttack)
                {
                    if (!UseSpecialAttack()) sM = StateMachine.RangeAttack;
                    else if (UseSpecialAttack() && DistanceY() >= stats.specialHighAttackMinY)
                    {
                        sM = StateMachine.SpecialHighAttack;    
                    }
                    else
                    {
                        sM = StateMachine.SpecialLowAttack;
                    }
                }
                    
                if (distanceFromPlayer < stats.closeRangeAttackThreshold && canAttack) 
                    sM = StateMachine.CloseAttack;
                break;
            case StateMachine.RangeAttack:
                NormalRangeAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.CloseAttack:
                NormalCloseAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.SpecialLowAttack:
                SpecialLowAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.SpecialHighAttack:
                SpecialHighAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.Sheild:
                Sheild();
                sM = StateMachine.Idle;
                break;
            default:
                break;
        }
    }
}
