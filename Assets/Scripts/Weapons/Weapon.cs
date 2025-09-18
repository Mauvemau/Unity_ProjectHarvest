using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon {
    [Header("Base Stats")]
    [field: SerializeField] public WeaponStats BaseStats { get; protected set; }
    [Header("Base Settings")]
    [SerializeField] private LayerMask targetLayer;

    protected Vector2 _aimDirection;
    protected float _nextAttack;

    public void AimWeapon(Vector2 direction) {
        _aimDirection = direction;
    }
}
