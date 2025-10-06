using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls a weapon automatically using a scanner
/// </summary>
[RequireComponent(typeof(IWeapon))]
public class AutoWeaponController : WeaponController {
    [Header("References")]
    [SerializeField] private Scanner scannerReference;

    [Header("Settings")]
    [SerializeField] private float pollingRate = 0.1f;

    private float _nextPoll = 0f;

    private void HandleTargetting() {
        GameObject closest = scannerReference.GetClosest(transform.position);
        if (!closest) {
            AimWeapon(Vector2.zero);
            return;
        }
        if (!closest.TryGetComponent(out IDamageable damageable)) return;

        Vector2 direction = ((Vector2)closest.transform.position - (Vector2)transform.position).normalized;

        AimWeapon(direction);
    }

    private void Update() {
        if (Time.time < _nextPoll) return;
        _nextPoll = Time.time + pollingRate;

        HandleTargetting();
    }

    protected override void OnAwake() {
        if (!scannerReference) {
            Debug.LogError($"{name}: missing required component {nameof(CircleCollider2D)}");
        }
    }
}
