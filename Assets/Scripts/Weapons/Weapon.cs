using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon {
    [Header("Base Stats")]
    [field: SerializeField] public WeaponStats BaseStats { get; protected set; }
    /*[HideInInspector]*/ protected WeaponStats currentStats;
    [Header("Base Settings")]
    [SerializeField] protected LayerMask targetLayer;

    protected Vector2 _aimDirection;
    protected float _nextAttack;

    private void InitWeaponStats() {
        currentStats = BaseStats;
    }

    public void AimWeapon(Vector2 direction) {
        _aimDirection = direction;
    }

    protected virtual void OnAwake() { }

    private void Awake() {
        InitWeaponStats();

        OnAwake();
    }
}
