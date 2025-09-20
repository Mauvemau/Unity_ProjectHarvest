using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon {
    [Header("Base Stats")]
    [field: SerializeField] public WeaponStats BaseStats { get; protected set; }
    protected WeaponStats currentStats;
    
    [Header("Base Settings")]
    [SerializeField] protected LayerMask targetLayer;

    protected Vector2 aimDirection;
    protected float nextAttack;

    private void InitWeaponStats() {
        currentStats = BaseStats;
        aimDirection = Vector2.right;
        nextAttack = 0;
    }

    public void AimWeapon(Vector2 direction) {
        aimDirection = direction;
    }

    protected virtual void OnAwake() { }

    private void Awake() {
        InitWeaponStats();

        OnAwake();
    }
}
