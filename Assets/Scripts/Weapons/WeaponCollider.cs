using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class WeaponCollider : Weapon {
    [Header("Visual Settings")] 
    [SerializeField] private bool drawRadiusGizmo = false;
    [SerializeField] private Color gizmoColor = Color.red;
    
    private CircleCollider2D _collider;

    private void HandleAttack(Collider2D other) {
        if (Time.time < nextAttack) return;
        nextAttack = Time.time + currentStats.attackRateInSeconds;
        
        if (!other.TryGetComponent(out IDamageable damageable)) return;
        
        damageable.TakeDamage(currentStats.attackDamage);
    }

    private void OnTriggerStay2D(Collider2D other) {
        HandleAttack(other);
    }

    protected override void OnAwake() {
        if (!TryGetComponent(out _collider)) {
            Debug.Log($"{name}: missing required component {nameof(CircleCollider2D)}");
        }
        _collider.isTrigger = true;
        _collider.radius = BaseStats.attackSize;
        
        const int everything = ~0;
        int inverted = everything & ~targetLayer;
        _collider.includeLayers = 0;
        _collider.excludeLayers = inverted;
    }
    
    private void OnDrawGizmos() {
        if (!drawRadiusGizmo) return;
        UnityEditor.Handles.color = gizmoColor;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, BaseStats.attackSize);
    }
}
