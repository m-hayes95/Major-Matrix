using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    // Stats
    public PlayerStatsScriptableObject stats;
    // Game object components
    private Rigidbody2D rb;
    private new BoxCollider2D collider;
    private PlayerHealth hp;
    private PlayerInput input;
    // Movemet
    private Vector3 moveDir;
    private bool facingRight = true;
    // Jump
    [SerializeField]private bool isGrounded;
    [SerializeField]private int jumpCount;
    // Gravity
    private float saveGravityScale;
    //Collisions
    private RaycastHit2D groundHit;
    private RaycastHit2D ceilingHit;
    private bool isCeilingHit = false;
    // CoyoteTime - Add coyote time
    //private bool coyoteTimeReady;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        hp = GetComponent<PlayerHealth>();
        input = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        saveGravityScale = rb.gravityScale;
    }
    private void FixedUpdate()
    {
        HandleMovement();
        CollisionChecks();
        FasterFallingOverTime();
    }
    private void HandleMovement()
    {
        // Apply movement using input
        moveDir = new Vector3(input.MovementInputNormalized().x, 0, 0);
        transform.position += moveDir * stats.moveSpeed * Time.deltaTime;
        if (!facingRight && moveDir.x > 0) TurnPlayer();
        if (facingRight && moveDir.x < 0) TurnPlayer();
    }
    private void TurnPlayer()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
    public void Jump()
    {
        if (jumpCount < 1 && isGrounded)
        {
            jumpCount++;
            ExecuteJump();
        }
    }
    private void ExecuteJump()
    {
        rb.velocity = Vector2.up * stats.jumpPower;
    }
    public void CancelJump()
    {
        if (rb.velocity.y > 0)
        {
            Debug.Log("End Jump");
            IncreaseGravityScale();
        }
    }
    private void IncreaseGravityScale()
    {
        rb.gravityScale *= 2;
    }

    private void FasterFallingOverTime()
    {
        if (DistanceFromFloor() >= stats.maxJumpThreshold)
        {
            rb.gravityScale = Mathf.MoveTowards(
                rb.gravityScale, stats.maxFallGravityScale,
                stats.fallGravityScaleMultiplier * Time.fixedDeltaTime
                );
        }
    }
    private void CollisionChecks()
    {
        //Debug.Log($"Is player grounded: {isGrounded}");
        float angle = 0;
        float distance = .2f;
        groundHit = Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.down, distance, stats.groundLayerMask
            );
        ceilingHit = Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.up, distance, stats.groundLayerMask
            );
        if (groundHit)
        {
            JumpReset();
        }
        else
        {
            isGrounded = false;
        }

        // check ceiling hit
        if (ceilingHit && !isCeilingHit)
        {
            isCeilingHit = true;
            CancelJump();
            //Debug.Log($"Ceiling hit: {ceilingHit} with {ceilingHit.collider.gameObject.name}");
        }
    }
    private float DistanceFromFloor()
    {
        float angle = 0f;
        float maxDistance = 50f;
        RaycastHit2D distanceChecker =
        Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.down, maxDistance, stats.groundLayerMask
            );
        float distance = distanceChecker.distance;
        //Debug.Log($"Players distance from floor: {distance}");
        return distance;
    }

    private void JumpReset()
    {
        isGrounded = true;
        jumpCount = 0;
        rb.gravityScale = saveGravityScale;
        isCeilingHit = false;
    }
}
