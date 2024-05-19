using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, apexJumpSpeedBonusMultiplier, apexJumpBoostDuration, apexJumpThreshold;
    [SerializeField] private float jumpForce;
    [SerializeField, Tooltip("How fast the gravity scale reaches falling gravity scale stat.")] 
    private float fallGravityScaleMultiplier;
    [SerializeField, Tooltip("The max gravity stat applied to falling when the jump button has been let go.")] 
    private float maxFallGravityScale;
    [SerializeField] float apexJumpGravityScale;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float initialGravityScale, initialMoveSpeed;
    private float apexJumpTimer;
    // Jump
    [SerializeField] private bool jumpInputPressed, jumpInputHeld;
    [SerializeField] private bool endedJumpEarly;
    private bool canJump;
    private bool applyApexJumpBoost = false;
    [SerializeField] private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set the gravity the player uses to reset fall when grounded
        initialGravityScale = rb.gravityScale;
        initialMoveSpeed = moveSpeed;
        
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
            Mathf.Abs(transform.position.y) >= apexJumpThreshold;
        // Variable jump height
        HandleGravity();
        // Check if jump ended early
        if (!isGrounded && !jumpInputHeld) 
            endedJumpEarly = true;
        // Jump input
        Vector2 jump = new Vector2 (0, jumpForce);
        if (jumpInputPressed || jumpInputHeld && canJump)
        {
            rb.AddForce(jump, ForceMode2D.Impulse);
        }
        if (jumpInputHeld && apexJumpThresholdAchieved)
        {
            JumpApexModifiers();
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Using scripts to find game objects instead of tags
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            isGrounded = true;
            endedJumpEarly = false;
            canJump = true;
            applyApexJumpBoost = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            isGrounded = false;
            canJump = false;
        }
    }
}
