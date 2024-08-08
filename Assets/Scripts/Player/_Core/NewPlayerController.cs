using System.Collections;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    // Stats
    [Tooltip ("Place the scriptable object for player stats here")]
    public PlayerStatsScriptableObject stats;
    // Game object components
    private Rigidbody2D rb;
    private new BoxCollider2D collider;
    private PlayerHealth hp;
    private PlayerInput input;
    private Animator animator;
    // Movemet
    private Vector3 moveDir;
    private bool facingRight = true;
    // Jump
    private bool isGrounded;
    private int jumpCount;
    // Gravity
    private float saveGravityScale;
    private bool isFalling = false;
    //Collisions
    private RaycastHit2D groundHit;
    private RaycastHit2D ceilingHit;
    private bool isCeilingHit = false;
    private float acceptanceDistanceRadius = 0.1f;
    // CoyoteTime - Add coyote time
    private bool coyoteTimeReady = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        hp = GetComponent<PlayerHealth>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        saveGravityScale = rb.gravityScale; // Store the original gravity scale
    }
    private void Update()
    {
        Debug.Log($"Jumping {jumpCount}");
    }
    private void FixedUpdate()
    {
        HandleMovement();
        CollisionChecks();
        FasterFallingOverTime();
        CheckIfFalling();
    }
    private void HandleMovement()
    {
        // Apply movement using input script
        moveDir = new Vector3(input.MovementInputNormalized().x, 0, 0);
        if (hp.GetIsDead()) return; // Dont allow movement if dead
        transform.position += moveDir * stats.moveSpeed * Time.deltaTime;
        if (moveDir != Vector3.zero) animator.SetBool("IsPlayerMoving", true);
        else animator.SetBool("IsPlayerMoving", false);
        // Change which way player is facing
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
        if (jumpCount == 0 && isGrounded || coyoteTimeReady)
        {
            ExecuteJump();
        }
    }
    private void CheckIfFalling()
    {
        if (rb.velocity.y < 0)
        {
            isFalling = true;
            if (jumpCount < 1)
                StartCoroutine(coyoteTimer());
        }

    }
    private IEnumerator coyoteTimer() // Allow player to jump for a set time after falling off a ledge
    {
        coyoteTimeReady = true;
        yield return new WaitForSeconds(.2f);
        coyoteTimeReady = false;
    }
    private void ExecuteJump()
    {
        if (!hp.GetIsDead())
        {
            animator.SetTrigger("PlayerJump");
            jumpCount++;
            rb.velocity = Vector2.up * stats.jumpPower;
        }
    }
    public void CancelJump() // Allow for short jumps (long press for higher jump)
    {
        if (rb.velocity.y > 0)
        {
            Debug.Log("End Jump");
            IncreaseGravityScale();
        }
    }
    private void IncreaseGravityScale() // Start falling
    {
        rb.gravityScale *= 2;
    }

    private void FasterFallingOverTime() // Increase falling speed over time
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
        // Check if grounded
        // Debug.Log($"Is player grounded: {isGrounded}");
        float angle = 0;
        groundHit = Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.down, acceptanceDistanceRadius, stats.groundLayerMask
            );
        ceilingHit = Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.up, acceptanceDistanceRadius, stats.groundLayerMask
            );
        if (groundHit)
        {
            if (isFalling) JumpReset();
        }
        else
        {
            isGrounded = false;
        }

        // check if ceiling hit
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
        isFalling = false;
        coyoteTimeReady = false;
        StopAllCoroutines();
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3 (collider.bounds.center.x, collider.bounds.center.y - acceptanceDistanceRadius), collider.size);
    }
    */
}
