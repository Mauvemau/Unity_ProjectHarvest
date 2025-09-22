using UnityEngine;

/// <summary>
/// Base class for weapons
/// </summary>
public abstract class Weapon : MonoBehaviour, IWeapon {
    [Header("Base Stats")]
    [field: SerializeField] public WeaponStats BaseStats { get; protected set; }
    protected WeaponStats CurrentStats;
    
    [Header("Base Settings")]
    [SerializeField] protected LayerMask targetLayer;

    protected Vector2 AimDirection;
    protected float NextAttack;

    private void InitWeaponStats() {
        CurrentStats = BaseStats;
        AimDirection = Vector2.right;
        NextAttack = 0;
    }

    public void AimWeapon(Vector2 direction) {
        AimDirection = direction;
    }

    protected virtual void OnAwake() { }

    private void Awake() {
        InitWeaponStats();

        OnAwake();
    }
}
