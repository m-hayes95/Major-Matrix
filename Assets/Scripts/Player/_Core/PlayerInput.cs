using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uIController;
    private PlayerInputActions inputActions;
    private NewPlayerController controller;
    private PlayerHealth hp;
    private Shoot shoot;
    private bool isGamePaused = false;
    private bool weaponPressed = false;
    private bool isFiring = false;

    private void OnEnable() { GameManager.OnPaused += UpdateIsGamePaused; inputActions.Player.Enable(); }
    private void OnDisable() { GameManager.OnPaused -= UpdateIsGamePaused; inputActions.Player.Disable(); }
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Jump.performed += JumpInputPerformed;
        inputActions.Player.Jump.canceled += JumpInputCanceled;
        inputActions.Player.CloseMenu.performed += PauseInputPerformed;
        inputActions.Player.Fire.performed += FireWeaponPerformed;
        inputActions.Player.Fire.canceled += FireWeaponCanceled;
        inputActions.Player.LoadNextScene.performed += ForceLoadNextScene;
    }
    private void Start()
    {
        controller = GetComponent<NewPlayerController>();
        hp = GetComponent<PlayerHealth>();
        shoot = GetComponent<Shoot>();
    }

    private void Update()
    {
        if (!gameManager.GetIsGamePaused() && !hp.GetIsDead() && 
            !weaponPressed && isFiring)
        {
            weaponPressed = true;
            shoot.FireWeaponPlayer(controller.stats.shotVelocity);
            StartCoroutine(ShotDelay());
        }
    }
    private void FireWeaponPerformed(InputAction.CallbackContext context)
    { 
        if (context.performed)
        {
            isFiring = true;
        }
            
    }
    private void FireWeaponCanceled(InputAction.CallbackContext context) 
    {
        if (context.canceled)
            isFiring = false;
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(.3f);
        weaponPressed = false;
    }

    private void PauseInputPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
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

    private void JumpInputPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Jump();
        }
    }
    private void JumpInputCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
            controller.CancelJump();
    }
    private void ForceLoadNextScene(InputAction.CallbackContext context)
    {
        if (context.performed)
            SceneLoadOrder.Instance.LoadNextScene();
    }
    public Vector2 MovementInputNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
