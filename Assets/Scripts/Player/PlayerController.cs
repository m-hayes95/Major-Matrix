using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, apexJumpSpeedBonusMultiplier, apexJumpBoostDuration, apexJumpThreshold;
    [SerializeField] private float jumpForce;
    [SerializeField] private float wallColliderRadius;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField, Tooltip("How fast the gravity scale reaches falling gravity scale stat.")] 
    private float fallGravityScaleMultiplier;
    [SerializeField, Tooltip("The max gravity stat applied to falling when the jump button has been let go.")] 
    private float maxFallGravityScale;
    [SerializeField] float apexJumpGravityScale;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Vector2 moveDir;
    private float initialGravityScale, initialMoveSpeed, initialJumpForce;
    private float apexJumpTimer;
    private RaycastHit2D groundHit;
    private RaycastHit2D ceilingHit;
    // Jump
    [SerializeField] private bool jumpInputPressed, jumpInputHeld;
    [SerializeField] private bool endedJumpEarly;
    private bool canJump;
    private bool applyApexJumpBoost = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool jumpReady;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        // Set the gravity the player uses to reset fall when grounded
        initialGravityScale = rb.gravityScale;
        initialMoveSpeed = moveSpeed;
        initialJumpForce = jumpForce;
    }

    
    private void Update()
    {
        GetInput();
        Debug.Log("Gravity scale = " + rb.gravityScale);
        //Debug.Log("Y pos = " + transform.position.y);
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        CollisionChecks();
        Debug.Log("Move speed " + moveSpeed);
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
        transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
        bool apexJumpThresholdAchieved =
            Mathf.Abs(DistanceFromFloor()) >= apexJumpThreshold;
        // Variable jump height
        HandleGravity();
        // Check if jump ended early
        if (!endedJumpEarly && !isGrounded && !jumpInputHeld && rb.velocity.y > 0) 
            endedJumpEarly = true;
        // Jump input
        if (jumpReady)
        {
            if (jumpInputPressed || jumpInputHeld && canJump)
            {
                ExecuteJump();
            }
            if (jumpInputHeld && apexJumpThresholdAchieved)
            {
                JumpApexModifiers();
            }
        }
    }

    private void ExecuteJump()
    {
        Vector2 jumpDirection = new Vector2(0, initialJumpForce);
        rb.velocity = Vector2.up * initialJumpForce;
    }

    private void HandleGravity()
    {
        // Increase player gravity when the jump button is released before landing
        if (!isGrounded && endedJumpEarly)
            rb.gravityScale = Mathf.MoveTowards(
                rb.gravityScale, maxFallGravityScale, 
                fallGravityScaleMultiplier * Time.fixedDeltaTime
                );
        if (isGrounded)
            rb.gravityScale = initialGravityScale;
    }

    private void JumpApexModifiers()
    {
        // Apply anti gravity and speed bost at the apex of the jump for greater control
        apexJumpTimer += Time.deltaTime;
        Debug.Log($"Apply apex threshold modifiers. Elapsed Time: {apexJumpTimer}");
        if (apexJumpTimer < apexJumpBoostDuration)
        {
            if (!applyApexJumpBoost)
            {
                applyApexJumpBoost = true;
                moveSpeed *= apexJumpSpeedBonusMultiplier;
                rb.gravityScale = apexJumpGravityScale;
            }
        }
        StartCoroutine(ResetApexJumpTimer(apexJumpBoostDuration));
    }

    private IEnumerator ResetApexJumpTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("Reset apex threshold modifiers");
        moveSpeed = initialMoveSpeed;
        rb.gravityScale = initialGravityScale;
        apexJumpTimer = 0;
    }

    private float DistanceFromFloor()
    {
        float angle = 0f;
        float maxDistance = 50f;
        RaycastHit2D distanceChecker =
        Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.down, maxDistance , groundLayerMask
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
            collider.bounds.center, collider.size, angle, Vector2.down, distance, groundLayerMask
            );
        ceilingHit = Physics2D.BoxCast(
            collider.bounds.center, collider.size, angle, Vector2.up, distance, groundLayerMask
            );
        
        RaycastHit2D wallHitLeft = Physics2D.Raycast(
            collider.bounds.center, Vector2.left, wallColliderRadius, groundLayerMask
            );
        RaycastHit2D wallHitRight = Physics2D.Raycast(
            collider.bounds.center, Vector2.right, wallColliderRadius, groundLayerMask
            );
        Debug.DrawRay(collider.bounds.center, Vector3.left * wallColliderRadius, UnityEngine.Color.red);
        if (groundHit)
        {
            isGrounded = true;
            Debug.Log($"Ground hit: {groundHit} with {groundHit.collider.gameObject.name}");
        }
        else
        {
            isGrounded = false;
        }

        // check ceiling hit
        if (ceilingHit)
        {
            jumpForce = 0f;
            Debug.Log($"Ceiling hit: {ceilingHit} with {ceilingHit.collider.gameObject.name}");
        }

        if (wallHitLeft || wallHitRight)
        {
            // Do not allow jump when collision occurs
            jumpReady = false;
            //Debug.Log($"Wall hit: {wallHitLeft} with {wallHitLeft.collider.gameObject.name}");
        }
        else
        {
            jumpReady = true;
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            //isGrounded = false;
            canJump = false;
        }
    }

    

}