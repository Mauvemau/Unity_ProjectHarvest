using UnityEngine;

public abstract class WeaponController : MonoBehaviour {
    protected IWeapon WeaponReference;

    protected void AimWeapon(Vector2 direction) {
        WeaponReference.AimWeapon(direction.normalized);
    }

    protected virtual void OnAwake() { }

    private void Awake() {
        if (!TryGetComponent(out WeaponReference)) {
            Debug.LogError($"{name}: missing reference \"{nameof(WeaponReference)}\"");
        }
        OnAwake();
    }
}
