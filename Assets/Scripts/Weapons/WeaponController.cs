using UnityEngine;

/// <summary>
/// Defines how a weapon is controlled by the player
/// </summary>
[RequireComponent(typeof(IWeapon))]
public class WeaponController : MonoBehaviour { 
    
    private IControllableCamera _mainCameraReference;
    private IWeapon _weaponReference;

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
    
    private void AimWeapon(Vector2 input) {
        TrySetCameraReference();
        if (input == Vector2.zero) return;
        if (_mainCameraReference == null) return;
        
        input = TranslateInput(input);
        
        _weaponReference.AimWeapon(input.normalized);
    }
    
    private void Awake() {
        if (!TryGetComponent(out _weaponReference)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_weaponReference)}\"");
        }
    }
    
    private void OnEnable() {
        InputManager.OnPlayerAimInputPerformed += AimWeapon;
    }

    private void OnDisable() {
        InputManager.OnPlayerAimInputPerformed -= AimWeapon;
    }
}
