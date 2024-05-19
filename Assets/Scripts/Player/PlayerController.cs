using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField, Tooltip("How fast the gravity scale reaches falling gravity scale stat.")] 
    private float fallGravityScaleMultiplier;
    [SerializeField, Tooltip("The gravity stat applied to falling when the jump button has been let go.")] 
    private float fallGravityScale;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float initialGravityScale;
    // Jump
    [SerializeField] private bool jumpInputPressed, jumpInputHeld;
    [SerializeField] private bool endedJumpEarly;
    private bool canJump;
    [SerializeField] private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set the gravity the player uses to reset fall when grounded
        initialGravityScale = rb.gravityScale;
        // Set the gravity increase when player released jump button early
        //fallGravityScale = rb.gravityScale * fallGravityMultiplier;
    }

    
    private void Update()
    {
        GetInput();
        Debug.Log("Gravity scale = " + rb.gravityScale);
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
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
        // Variable jump height
        HandleGravity(); 
        if (!isGrounded && !jumpInputHeld) 
            endedJumpEarly = true;
        // Jump input
        Vector2 jump = new Vector2 (0, jumpForce);
        if (jumpInputPressed || jumpInputHeld && canJump)
            rb.AddForce(jump, ForceMode2D.Impulse);
    }

    private void HandleGravity()
    {
        // Increase player gravity when the jump button is released before landing
        if (!isGrounded && endedJumpEarly)
            rb.gravityScale = Mathf.MoveTowards(
                rb.gravityScale, fallGravityScale, 
                fallGravityScaleMultiplier * Time.fixedDeltaTime
                );
        if (isGrounded)
            rb.gravityScale = initialGravityScale;
    }

    private void ApexModifiers()
    {
        // Apply anti gravity and speed bost at the apex of the jump for greater control
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Using scripts to find game objects instead of tags
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            isGrounded = true;
            endedJumpEarly = false;
            canJump = true;
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
