using UnityEngine;

/// <summary>
/// Defines how a weapon is controlled by the player reading inputs
/// </summary>
[RequireComponent(typeof(IWeapon))]
public class InputWeaponController : WeaponController {
    private IControllableCamera _mainCameraReference;

    private void TrySetCameraReference() {
        if (_mainCameraReference != null) return;
        if (!ServiceLocator.TryGetService(out _mainCameraReference)) return;
    }
    
    private Vector2 TranslateInput(Vector2 input) {
        Vector2 direction = input;
        if (input.x > 1f || input.y > 1f) {
            Vector3 mousePos = _mainCameraReference.GetScreenToWorldPoint(direction);
            direction = ((Vector2)mousePos - (Vector2)transform.position).normalized;
        }
        else {
            direction = direction.normalized;
        }
        return direction;
    }

    private void HandleInput(Vector2 input) {
        TrySetCameraReference();
        if (input == Vector2.zero) return;
        if (_mainCameraReference == null) return;

        AimWeapon(TranslateInput(input));
    }
    
    private void OnEnable() {
        InputManager.OnPlayerAimInputPerformed += HandleInput;
    }

    private void OnDisable() {
        InputManager.OnPlayerAimInputPerformed -= HandleInput;
    }
}
