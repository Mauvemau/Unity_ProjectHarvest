using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [Header("Player Actions")]
    [SerializeField] private InputActionReference playerMoveAction;
    [SerializeField] private InputActionReference playerAimAction;
    [SerializeField] private InputActionReference playerInteractAction;

    [Header("UI Actions")] 
    [SerializeField] private InputActionReference uiQuitProgramAction;

    public static event Action<Vector2> OnPlayerMoveInputPerformed = delegate {};
    public static event Action<Vector2> OnPlayerAimInputPerformed = delegate {};
    public static event Action OnPlayerInteractInputPerformed = delegate {};

    private bool _shouldReadPlayerInput = false;

    // Input Handler Functions

    // Player
    
    private void HandlePlayerMoveInput(InputAction.CallbackContext ctx) {
        if (!_shouldReadPlayerInput) return;
        OnPlayerMoveInputPerformed?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void HandlePlayerAimInput(InputAction.CallbackContext ctx) {
        if (!_shouldReadPlayerInput) return;
        OnPlayerAimInputPerformed?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void HandlePlayerInteractInput(InputAction.CallbackContext ctx) {
        if (!_shouldReadPlayerInput) return;
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
