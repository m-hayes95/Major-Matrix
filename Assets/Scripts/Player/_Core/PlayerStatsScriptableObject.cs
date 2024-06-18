using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObjects/Player Stats")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    // Stats for the player game object

    [Header("Player Stats")]
    [Range(1f, 200f), Tooltip("How much HP the player will start with.")]
    public float maxHP;
    [Range(1f, 10f), Tooltip("How fast the payer will move.")]
    public float moveSpeed;
    [Range(5f, 30f), Tooltip("How much power the player will use to jump.")]
    public float jumpPower;
    [Range(0f, 100f), Tooltip("How much damage each shot does.")]
    public float weaponPower;
    [Range(1f, 20f), Tooltip("How much velocity each shot has.")]
    public float shotVelocity;

    [Header("Gravity Stats")]
    [Range(1f, 40f), Tooltip("How far the player can jump before gravity modifers take place (Adjust, depending on Jump Power stat).")]
    public float maxJumpThreshold;
    [Range(1f, 50f), Tooltip("The max gravity scale that can be applied to player's jump fall (How fast they will fall).")]
    public float maxFallGravityScale;
    [Range(1f, 50f), Tooltip("How fast the player's fall will reach the max gravity scale above (The higher the number, the quicker it will reach it).")]
    public float fallGravityScaleMultiplier;

    [Header("Coyote Time Stats")]
    [Range(0f, 1f), Tooltip("How long coyote time will stay available for use, after the player has left the ground.")]
    public float coyoteTimeThreshold;

    [Header("Apex Jump Modifier Stats")]
    [Range(0f, 10f), Tooltip("The height of the jump where the jump stats should be modified (Adjust, depending on Jump Power stat).")]
    public float apexJumpThreshold;
    [Range (0f, 1f), Tooltip("How long the jump stats will be modified for when the player's jump reaches the apex point.")]
    public float apexJumpBoostDuration;
    [Range (0f, 1f), Tooltip("How much gravity the player will have when they have reached the jumps apex.")]
    public float apexJumpGravityScale;
    [Range (0f, 5f), Tooltip("Multiply the player's speed when the reach the jumps apex for the apex jump boost duration amount.")]
    public float apexJumpSpeedBonusMultiplier;

    [Header("Collision Stats")]
    [Tooltip("Checks to see if the game object the player is colliding with is the ground (Select Ground and make sure the platform has the ground layer mask attribute selected - Works with Distance from floor checks and grounded collision checks).")]
    public LayerMask groundLayerMask;
    [Range(0f, 0.5f), Tooltip("How far from the ground of ceiling the player will be to activate collisions (for grounded and ceiling checks).")]
    public float collisionDistanceAcceptanceRadius;
}
