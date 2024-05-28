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

    [Header("Thresholds")]
    [Range(0f, 5f), Tooltip("How close the player needs to be, to activate the close range attack.")]
    public float closeRangeAttackThreshold;
    [Range(0f, 20f), Tooltip("How far the player needs to be, to activate the long range attack.")]
    public float longRangeAttackThreshold;
    [Range(0f, 10f), Tooltip("Only applys to special attacks. Set the min amount for the player's height to determine if the Boss should use a High attack instead of a low attack (Uses difference in Y poistion from the player to the boss character. The higher the value, the higher the player needs to be in order to call the high special attacks).")]
    public float specialHighAttackMinY;
    [Range(0f, 200f), Tooltip("Set the max HP the boss is allowed to shield from (E.g if set to 50, the boss can only shield when their health is 50 or less).")]
    public float maxHPtoAllowShield;

    [Header("Chances")]
    [Range(0f, 1f), Tooltip("Select the chance of a special attack occuring over a normal attack when the players distance meets the longRangeAttackThreshold (0 = no chance, 0.1 = 1/10 chance, 0.8 = 8/10 chance, so on...)")]
    public float chanceToUseSpecialAttack;
    [Range(0f, 1f), Tooltip("Set the chance the enemy will shield at the set maxHPtoAllowShield threshold")]
    public float chanceToShield;

    [Header("Timers")]
    [Range(0f, 10f), Tooltip("Set the amount of time in seconds for the reset attack timer.")]
    public float resetAttackTimer;
    [Range(0f, 10f), Tooltip("Set the amount of time in seconds for the shield duration.")]
    public float shieldTimer;

    [Header("Cooldowns")]
    [Range(0f, 50f), Tooltip("Set the amount of time (in seconds) for the shield cooldown (Boss can not shield again during the cooldown period).")]
    public float shieldCooldownTime;
    [Range(0f, 10f), Tooltip("Set how long the enemy will chase the player before stoping (in seconds).")]
    public float chaseTimer;
    [Range(0f, 20f), Tooltip("set the amount of time (in seconds), for how long the boss must wait before they are able to chase the player again")]
    public float chaseCooldown;

    [Header("Special Attack Stats")]
    [Range(1, 5), Tooltip("Set the amount of attacks each special attack has.")]
    public int numberOfAttacks;
    [Range(-1f,1f), Tooltip("Set the value of the next attack's Y offset.")]
    public float offsetY;
    [Range(1f, 5f), Tooltip("Set the value of the space between each attack on the X axis.")]
    public float spacingX;
}

