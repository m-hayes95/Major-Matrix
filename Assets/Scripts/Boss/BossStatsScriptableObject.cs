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

    [Header("Thresholds")]
    [Range(0f, 5f), Tooltip("How close the player needs to be, to activate the close range attack.")]
    public float closeRangeAttackThreshold;
    [Range(0f, 20f), Tooltip("How far the player needs to be, to activate the long range attack.")]
    public float longRangeAttackThreshold;
}
