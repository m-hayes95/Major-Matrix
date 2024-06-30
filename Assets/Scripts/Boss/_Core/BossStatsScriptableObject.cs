using UnityEngine;

[CreateAssetMenu(fileName = "Boss Stats", menuName = "ScriptableObjects/Boss Stats")]
public class BossStatsScriptableObject : ScriptableObject
{
    

    [Header("Boss Stats")]
    [Range(1f,20f), Tooltip("How fast the boss character will move.")]
    public float moveSpeed;
    [Range(1f, 200f), Tooltip("How much HP the boss character will start with.")]
    public float maxHP;
    [Range(0f, 100f), Tooltip("How much damage the normal attacks will do to the player.")]
    public float normalAttackDamage;
    [Range(0f, 100f), Tooltip("How much damgae the special attacks will do to the player.")]
    public float specialAttackDamage;
    [Range(0, 10), Tooltip("How many shields the boss will start with. Once shields reach 0, the boss cannot shield again.")]
    public int maxShields;
    [Range(0f, 40f), Tooltip("How much force should be applied to the bullets when shot.")]
    public float shotFoce;
    [Range(0f, 50f), Tooltip("How far the BT boss can see the target.")]
    public float bossFOV_BT;
    [Range(0f, 50f), Tooltip("How far the FSM boss can see the target.")]
    public float bossFOV_FSM;
    [Tooltip("Set the target of the boss through Game object Layers.")]
    public LayerMask targetLayerMask;

    [Header("Thresholds")]
    [Range(0f, 50f), Tooltip("How far the player needs to move from the boss to start chasing.")]
    public float chaseDistanceThreshold;
    [Range(0f, 5f), Tooltip("How close the player needs to be, to activate the close range attack.")]
    public float meleeAttackRange;
    [Range(0f, 20f), Tooltip("How far the player needs to be, to activate the long range attack.")]
    public float longRangeAttackThreshold;
    [Range(0f, 10f), Tooltip("Only applys to special attacks. Set the min amount for the player's height to determine if the Boss should use a High attack instead of a low attack (Uses difference in Y poistion from the player to the boss character. The higher the value, the higher the player needs to be in order to call the high special attacks).")]
    public float specialHighAttackMinY;
    [Range(0f, 200f), Tooltip("Set the level of health were the boss will start attempting to save itself (from which HP should shields be used).")]
    public float dangerThreshold;

    [Header("Chances")]
    [Range(0f, 100f), Tooltip("Select the chance of a special attack occuring over a normal attack when the players distance meets the longRangeAttackThreshold (0 to 100% chance).")]
    public float chanceToUseSpecialAttack;
    [Range(0f, 100f), Tooltip("How much percentage increase should be applied to the chance to use special attacks, for specific circumstances.")]
    public float percentIncrease;
    [Range(0f, 1f), Tooltip("Set the chance the enemy will shield at the set maxHPtoAllowShield threshold.")]
    public float chanceToShield;

    [Header("Timers")]
    [Range(0f, 10f), Tooltip("Set the amount of time in seconds for the reset melee and normal range attack timer.")]
    public float resetNormalAttackTimer;
    [Range(0f, 10f), Tooltip("Set the amount of time in seconds for the reset special attack timers.")]
    public float resetSpecialAttackTimer;
    [Range(0f, 10f), Tooltip("Set the amount of time in seconds for the shield duration.")]
    public float shieldTimer;

    [Header("Cooldowns")]
    [Range(0f, 50f), Tooltip("Set the amount of time (in seconds) for the shield cooldown (Boss can not shield again during the cooldown period).")]
    public float shieldCooldownTime;
    [Range(0f, 10f), Tooltip("Set how long the enemy will chase the player before stoping (in seconds).")]
    public float chaseTimer;
    [Range(0f, 20f), Tooltip("set the amount of time (in seconds), for how long the boss must wait before they are able to chase the player again.")]
    public float chaseCooldown;

    [Header("Special Attack Stats")]
    [Range(1, 10), Tooltip("Set the amount of attacks each special attack has.")]
    public int numberOfAttacks;
    [Range(-1f,1f), Tooltip("Set the value of the next attack's Y offset.")]
    public float offsetY;
    [Range(1f, 5f), Tooltip("Set the value of the space between each attack on the X axis.")]
    public float spacingX;
    [Range(0f, 1f), Tooltip("How much time (in seconds) should each interval of attacks be.")]
    public float timeBetweenAttacks;

    [Header("High Special Attack Modifiers")]
    [Range(0f, 10f), Tooltip("How much gravity is applied to the falling high special attacks.")]
    public float highSpecialAttackGravityScale;
    [Range(0f, 20f), Tooltip("How high up the high special attack will spawn.")]
    public float spawnHeight;
}

