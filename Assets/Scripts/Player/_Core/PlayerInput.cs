using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController controller;
    private PlayerHealth hp;
    private Shoot shoot;
    private bool jumpInputPressed;
    private bool jumpInputHeld;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        hp = GetComponent<PlayerHealth>();
        shoot = GetComponent<Shoot>();  
    }

    private void Update()
    {
        FireWeaponInput();
    }
    private void FireWeaponInput()
    { 
        if (controller != null && !hp.GetIsDead())
        {
            if (Input.GetMouseButtonDown(0))
            {
                shoot.FireWeaponPlayer(controller.stats.shotVelocity);
            }
        }
    }

    public bool GetJumpInputPressed()
    {
        return jumpInputPressed = Input.GetButtonDown("Jump");
    }
    public bool GetJumpInputHeld()
    {
        return jumpInputHeld = Input.GetButton("Jump");
    }

    public Vector2 MovementInputNormalized()
    {
        Vector2 input = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A)) input.x = -1;
        if (Input.GetKey(KeyCode.D)) input.x = +1;
        input = input.normalized;
        return input;
    }
}