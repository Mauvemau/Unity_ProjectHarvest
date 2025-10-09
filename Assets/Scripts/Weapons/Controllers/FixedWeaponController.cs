using UnityEngine;

/// <summary>
/// Aims constantly at a static direction
/// </summary>
[RequireComponent(typeof(IWeapon))]
public class FixedWeaponController : WeaponController {
    [SerializeField] private Vector2 fixedAimDirection = Vector2.right;

    const float delay = 2f;

    private void FixAim() {
        AimWeapon(fixedAimDirection);
    }

    protected override void OnAwake() {
        Invoke(nameof(FixAim), delay);
    }
}