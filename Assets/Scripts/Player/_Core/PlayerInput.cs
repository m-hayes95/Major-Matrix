using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
// This class reads the input values and calls for code to be executed within the player controller
public class PlayerInput : MonoBehaviour
{
    // References
    [SerializeField, Tooltip("Add reference for the game manager to this field")] 
    private GameManager gameManager;
    [SerializeField, Tooltip("Add reference for the UI controller to this field")] 
    private UIController uIController;
    private PlayerInputActions inputActions;
    private NewPlayerController controller;
    private PlayerHealth hp;
    private Shoot shoot;
    // Checks
    private bool isGamePaused = false;
    private bool weaponPressed = false;
    private bool isFiring = false;

    private void OnEnable() { GameManager.OnPaused += UpdateIsGamePaused; inputActions.Player.Enable(); }
    private void OnDisable() { GameManager.OnPaused -= UpdateIsGamePaused; inputActions.Player.Disable(); }
    private void Awake()
    {
        // Subscribe to the player actions for new input system
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
        // Fire weapon on update, so player can hold shoot button down
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
        if (context.performed) // Start firing on update
        {
            isFiring = true;
        }
            
    }
    private void FireWeaponCanceled(InputAction.CallbackContext context) 
    {
        if (context.canceled) // Stop firing on update
            isFiring = false;
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(controller.stats.shotDelay);
        weaponPressed = false;
    }

    private void PauseInputPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!uIController.GetIsTabsOpen()) // Open pause menu
            {
                gameManager.PauseGame();
                return;
            }
            else // Close current menu tabs
            {
                uIController.DisplayPauseMenu();
                uIController.CloseOpenTabs(); 
                return;
            }
        }
    }

    private void UpdateIsGamePaused()
    {
        isGamePaused = !isGamePaused;
    }

    private void JumpInputPerformed(InputAction.CallbackContext context)
    {
        if (context.performed && !gameManager.GetIsGamePaused())
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
        if (context.performed) // Used to skip current boss fight if too difficult
            SceneLoadOrder.Instance.LoadNextScene();
    }
    public Vector2 MovementInputNormalized() // Read input vector 2 values
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
