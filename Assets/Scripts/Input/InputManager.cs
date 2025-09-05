using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [Header("Player Action")]
    [SerializeField] private InputActionReference playerMoveAction;
    [SerializeField] private InputActionReference playerAimAction;
    [SerializeField] private InputActionReference playerInteractAction;

    public static event Action<Vector2> OnPlayerMoveInputPerformed = delegate {};
    public static event Action<Vector2> OnPlayerAimInputPerformed = delegate {};
    public static event Action OnPlayerInteractInputPerformed = delegate {};

    private bool _shouldReadPlayerInput = false;

    // Input Handler Functions

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
    }

    private void OnDisable() {
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
    }
}
