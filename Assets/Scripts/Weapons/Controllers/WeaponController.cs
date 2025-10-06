using UnityEngine;
using UnityEngine.Windows;

public abstract class WeaponController : MonoBehaviour {
    protected IWeapon _weaponReference;

    protected void AimWeapon(Vector2 direction) {
        _weaponReference.AimWeapon(direction.normalized);
    }

    protected virtual void OnAwake() { }

    private void Awake() {
        if (!TryGetComponent(out _weaponReference)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_weaponReference)}\"");
        }
        OnAwake();
    }
}
