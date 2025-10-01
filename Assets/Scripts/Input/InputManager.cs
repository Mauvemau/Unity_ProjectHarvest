using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [Header("Player Actions")]
    [SerializeField] private InputActionReference playerMoveAction;
    [SerializeField] private InputActionReference playerAimAction;
    [SerializeField] private InputActionReference playerAimContinuousAction;
    [SerializeField] private InputActionReference playerInteractAction;

    [Header("UI Actions")] 
    [SerializeField] private InputActionReference uiQuitProgramAction;

    [Header("Settings")] 
    [SerializeField, Range(0f, 1f)] private float stickDeadZone = .2f;

    public static event Action<Vector2> OnPlayerMoveInputPerformed = delegate {};
    public static event Action<Vector2> OnPlayerAimInputPerformed = delegate {};
    public static event Action OnPlayerInteractInputPerformed = delegate {};

    private bool _shouldReadPlayerInput = false;
    private bool _shouldReadMouseInput = true;

    // Tools
    
    private Vector2 ApplyDeadZone(Vector2 input) {
        if (input.magnitude < stickDeadZone) {
            return Vector2.zero;
        }

        return input.normalized * ((input.magnitude - stickDeadZone) / (1 - stickDeadZone));
    }

    private bool IsGamePaused() {
        return Time.timeScale <= 0f;
    }
    
    private bool ShouldReadPlayerInput() {
        return _shouldReadPlayerInput;
    }
    
    // Input Handler Functions

    // Player
    
    private void HandlePlayerMoveInput(InputAction.CallbackContext ctx) {
        if (!ShouldReadPlayerInput()) return;
        OnPlayerMoveInputPerformed?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void HandlePlayerAimInputMouse(InputAction.CallbackContext ctx) {
        if (!_shouldReadMouseInput) return;
        if (IsGamePaused()) return;
        if (!ShouldReadPlayerInput()) return;
        
        OnPlayerAimInputPerformed?.Invoke(ctx.ReadValue<Vector2>());
    }
    
    private void HandlePlayerAimInput(InputAction.CallbackContext ctx) {
        if (IsGamePaused()) return;
        if (!ShouldReadPlayerInput()) return;
        _shouldReadMouseInput = !ctx.action.inProgress;
        
        Vector2 direction = ApplyDeadZone(ctx.ReadValue<Vector2>());
        
        OnPlayerAimInputPerformed?.Invoke(direction);
    }

    private void HandlePlayerInteractInput(InputAction.CallbackContext ctx) {
        if (!ShouldReadPlayerInput()) return;
        OnPlayerInteractInputPerformed?.Invoke();
    }

    // UI

    private void HandleQuitProgramInput(InputAction.CallbackContext ctx) {
        Application.Quit();
    }
    
    // Init

    private void Init() {
        _shouldReadPlayerInput = true;
    }

    private void Reset() {
        Init();
    }

    private void Awake() {
        Init();
    }

    // Input Events

    private void OnEnable() {
        // Player Actions
        if (playerMoveAction) {
            playerMoveAction.action.started += HandlePlayerMoveInput;
            playerMoveAction.action.performed += HandlePlayerMoveInput;
            playerMoveAction.action.canceled += HandlePlayerMoveInput;
        }
        if (playerAimAction) {
            playerAimAction.action.started += HandlePlayerAimInput;
            playerAimAction.action.performed += HandlePlayerAimInput;
            playerAimAction.action.canceled += HandlePlayerAimInput;
        }
        if (playerAimContinuousAction) {
            playerAimContinuousAction.action.performed += HandlePlayerAimInputMouse;
        }
        if (playerInteractAction) {
            playerInteractAction.action.started += HandlePlayerInteractInput;
        }
        
        // UI Actions
        if (uiQuitProgramAction) {
            uiQuitProgramAction.action.started += HandleQuitProgramInput;
        }
    }

    private void OnDisable() {
        // Player Actions
        if (playerMoveAction) {
            playerMoveAction.action.started -= HandlePlayerMoveInput;
            playerMoveAction.action.performed -= HandlePlayerMoveInput;
            playerMoveAction.action.canceled -= HandlePlayerMoveInput;
        }
        if (playerAimAction) {
            playerAimAction.action.started -= HandlePlayerAimInput;
            playerAimAction.action.performed -= HandlePlayerAimInput;
            playerAimAction.action.canceled -= HandlePlayerAimInput;
        }
        if (playerAimContinuousAction) {
            playerAimContinuousAction.action.performed -= HandlePlayerAimInputMouse;
        }
        if (playerInteractAction) {
            playerInteractAction.action.started -= HandlePlayerInteractInput;
        }
        
        // UI Actions
        if (uiQuitProgramAction) {
            uiQuitProgramAction.action.started -= HandleQuitProgramInput;
        }
    }
}
