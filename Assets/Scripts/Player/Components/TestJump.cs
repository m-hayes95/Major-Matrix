using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    Vector2 moveDir;
    bool jumpInputPressed, jumpInputHeld;
    [SerializeField] float moveSpeed;
    [SerializeField] float initialJumpForce;
    [SerializeField] bool isGrounded, canJump;
    [SerializeField] LayerMask groundLayerMask;
    Rigidbody2D Rigidbody2D;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        // Movement Input
        float horizontalInput = Input.GetAxis("Horizontal");
        moveDir = new Vector2(horizontalInput, 0);
        // Jump Input
        jumpInputPressed = Input.GetButtonDown("Jump");
        jumpInputHeld = Input.GetButton("Jump");
    }
    private void FixedUpdate()
    {
        CollisionChecks();
        HandleMovement();
        HandleJump();
    }
    
    private void HandleMovement()
    {
        // Apply movement using input
        transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
        if (jumpInputHeld && isGrounded && canJump)
        {
            canJump = false;
            ExecuteJump();
        }
    }

    private void ExecuteJump()
    {
        Vector2 jumpDirection = new Vector2(0, initialJumpForce);
        Rigidbody2D.velocity = Vector2.up * jumpDirection.y;
    }

    private void CollisionChecks()
    {
        float angle = 0;
        float distance = .2f;
        RaycastHit2D groundHit = Physics2D.BoxCast(
            col.bounds.center, col.bounds.size, angle, Vector2.down, distance, groundLayerMask
            );

        if (groundHit)
        {
            isGrounded = true;
            canJump = true;
            Debug.Log($"Ground hit: {groundHit} with {groundHit.collider.gameObject.name}");
        }
        else
        {
            isGrounded = false;
        }

        
    }
}
