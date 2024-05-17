using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }
    private void Movement()
    {
        // Apply movement using input
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDir = new Vector2(horizontalInput, 0);
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    private void Jump()
    { 
        Vector2 jump = new Vector2 (0, jumpForce);
        rb.AddForce(jump, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Using scripts to find game objects instead of tags
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GroundTag>())
        {
            isGrounded = false;
        }
    }
}
