using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uIController;
    private PlayerController controller;
    private PlayerHealth hp;
    private Shoot shoot;
    private bool jumpInputPressed;
    private bool jumpInputHeld;
    private bool isGamePaused = false;

    private void OnEnable() { GameManager.OnPaused += UpdateIsGamePaused; }
    private void OnDisable() { GameManager.OnPaused -= UpdateIsGamePaused; }
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        hp = GetComponent<PlayerHealth>();
        shoot = GetComponent<Shoot>();  
    }

    private void Update()
    {
        FireWeaponInput();
        PauseInput();
    }
    private void FireWeaponInput()
    { 
        if (controller != null && !hp.GetIsDead() && !isGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shoot.FireWeaponPlayer(controller.stats.shotVelocity);
            }
        }
    }

    private void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!uIController.GetIsTabsOpen()) gameManager.PauseGame();
            else
            {
                uIController.DisplayPauseMenu();
                uIController.CloseOpenTabs();
            }
        }
    }

    private void UpdateIsGamePaused()
    {
        isGamePaused = !isGamePaused;
    }

    public bool GetJumpInputPressed()
    {
        if (!isGamePaused) return jumpInputPressed = Input.GetButtonDown("Jump");
        else return false;
    }
    public bool GetJumpInputHeld()
    {
        if (!isGamePaused) return jumpInputHeld = Input.GetButton("Jump");
        else return false;
    }

    public Vector2 MovementInputNormalized()
    {
        if (!isGamePaused)
        {
            Vector2 input = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.A)) input.x = -1;
            if (Input.GetKey(KeyCode.D)) input.x = +1;
            input = input.normalized;
            return input;
        }
        else return Vector2.zero;
    }
}
