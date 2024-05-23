using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stats
    [SerializeField] private PlayerStatsScriptableObject stats;
    // Game object components
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    // Movemet
    private Vector2 moveDir;
    // Jump
    private bool jumpInputPressed, jumpInputHeld;
    private bool endedJumpEarly;
    private bool canJump;
    // Apex Jump
    private bool applyApexJumpBoost = false;
    private float apexJumpTimer;
    // Coyote Jump
    private bool coyoteTimeReady;
    private float coyoteTimeThresehold;
    //Collisions
    private RaycastHit2D groundHit;
    private RaycastHit2D ceilingHit;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isCeilingHit = false;
    // Set initial stat values
    private float initialGravityScale;
    private float initialMoveSpeed; 
    private float initialJumpPower;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        // Set the gravity the player uses to reset fall when grounded
        initialGravityScale = rb.gravityScale;
        initialMoveSpeed = stats.moveSpeed;
        initialJumpPower = stats.jumpPower;
    }


    private void Update()
    {
        GetInput();
        Debug.Log("Gravity scale = " + rb.gravityScale);
        //Debug.Log("Y pos = " + transform.position.y);
        Debug.Log($"Current Jump power {stats.jumpPower}, Initial Jump power {initialJumpPower}");
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        CollisionChecks();
    }
    private void GetInput()
    {
        // Movement Input
        float horizontalInput = Input.GetAxis("Horizontal");
        moveDir = new Vector2(horizontalInput, 0);
        // Jump Input
        jumpInputPressed = Input.GetButtonDown("Jump");
        jumpInputHeld = Input.GetButton("Jump");
        //Debug.Log("Jump pressed:" + jumpInputPressed);
        //Debug.Log("Jump held:" + jumpInputHeld);
    }
    private void HandleMovement()
    {
        // Apply movement using input
        transform.Translate(moveDir * stats.moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
        //bool apexJumpThresholdAchieved = Mathf.Abs(DistanceFromFloor()) >= stats.apexJumpThreshold;
        // Variable jump height
        HandleGravity();
        // Check if jump ended early
        if (!endedJumpEarly && !isGrounded && !jumpInputHeld && rb.velocity.y > 0) 
            endedJumpEarly = true;
        
        // Jump input
        if (jumpInputPressed || jumpInputHeld && canJump)
        {
            ExecuteJump();
        }

        // Coyote time jump - can jump a short time after falling off a platform
        else if (jumpInputPressed || jumpInputHeld && coyoteTimeReady)
        {
            coyoteTimeReady = false;
            Debug.Log("Coyote time jump executed");
            ExecuteJump();
        }

        // Appex Jump - When the peak of the jump height is reached
        /*
        if (jumpInputHeld && apexJumpThresholdAchieved)
        {
            //JumpApexModifiers();
        }
        */
    }
    private IEnumerator CoyoteTimeTimer()
    {
        yield return new WaitForSeconds(coyoteTimeThresehold);
        Debug.Log($"Coyote time is ready: {coyoteTimeReady}");
        if (!isGrounded) coyoteTimeReady = false;
    }

    private void ExecuteJump()
    {
        rb.velocity = Vector2.up * stats.jumpPower; 
    }
    private void HandleGravity()
    {
        // Increase player gravity when the jump button is released before landing, or when the hieght of the jump is reached
        if (DistanceFromFloor() >= stats.maxJumpThreshold) 
            endedJumpEarly = true;

        if (!isGrounded && endedJumpEarly)
            rb.gravityScale = Mathf.MoveTowards(
                rb.gravityScale, stats.maxFallGravityScale, 
                stats.fallGravityScaleMultiplier * Time.fixedDeltaTime
                );

        if (isGrounded)
            rb.gravityScale = initialGravityScale;
    }

    private void JumpApexModifiers()
    {
        // Apply anti gravity and speed bost at the apex of the jump for greater control
        apexJumpTimer += Time.deltaTime;
        Debug.Log($"Apply apex threshold modifiers. Elapsed Time: {apexJumpTimer}");
        if (apexJumpTimer < stats.apexJumpBoostDuration)
        {
            if (!applyApexJumpBoost)
            {
                applyApexJumpBoost = true;
                stats.moveSpeed *= stats.apexJumpSpeedBonusMultiplier;
                rb.gravityScale = stats.apexJumpGravityScale;
            }
        }
        StartCoroutine(ResetApexJumpTimer(stats.apexJumpBoostDuration));
    }

    private IEnumerator ResetApexJumpTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("Reset apex threshold modifiers");
        stats.moveSpeed = initialMoveSpeed;
        rb.gravityScale = initialGravityScale;
        apexJumpTimer = 0;
    }

  

    private float DistanceFromFloor()
    {
        float angle = 0f;
        float maxDistance = 50f;
        RaycastHit2D distanceChecker =
        Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.down, maxDistance , stats.groundLayerMask
            );
        float distance = distanceChecker.distance;
        Debug.Log($"Players distance from floor: {distance}");
        return distance;
    }

    private void CollisionChecks()
    {
        Debug.Log($"Is player grounded: {isGrounded}");
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
            isGrounded = true;
            isCeilingHit = false;
            stats.jumpPower = initialJumpPower;
            Debug.Log($"Ground hit: {groundHit} with {groundHit.collider.gameObject.name}");
        }
        else
        {
            isGrounded = false;
        }

        // check ceiling hit
        if (ceilingHit && !isCeilingHit)
        {
            isCeilingHit = true;
            stats.jumpPower = 0f;
            Debug.Log($"Ceiling hit: {ceilingHit} with {ceilingHit.collider.gameObject.name}");
        }
    }
    /*
    private void OnDrawGizmos()
    {
        if (groundHit)
        {
            Gizmos.color = UnityEngine.Color.green;
            Gizmos.DrawWireCube(
                collider.bounds.center + Vector3.down * .2f, collider.size
                );
        }
        else
        {
            Gizmos.color = UnityEngine.Color.yellow;
            Gizmos.DrawRay(transform.position, Vector3.down * 100f);
            Gizmos.DrawWireCube(
                collider.bounds.center + Vector3.down * .2f, collider.size
                );
        }
    }
    */


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Using scripts to find game objects instead of tags
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            //isGrounded = true;
            endedJumpEarly = false;
            canJump = true;
            applyApexJumpBoost = false;
            coyoteTimeReady = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            //isGrounded = false;
            canJump = false;
            Debug.Log($"Coyote time timer started");
            StartCoroutine(CoyoteTimeTimer());
        }
    }

    

}
