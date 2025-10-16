using UnityEngine;

public class WeaponRanged : Weapon {
    [Header("Factory Settings")]
    [SerializeField] private Factory bulletFactory;

    [Header("Bullet Settings")]
    [SerializeField] private BulletPresetSO preset;
    [SerializeField] private BulletStats bulletStats;
    [SerializeField] private float firstShotOffset = 0f;
    [SerializeField] private bool useParentTransform = false;
    
    private void Shoot(IBullet bullet, BulletPresetSO bulletPreset, Vector2 direction, float damage, BulletStats stats) {
        bullet.Shoot(bulletPreset, direction, targetLayer, damage, stats, useParentTransform ? transform.parent.transform : gameObject.transform);
    }
    
    private void HandleAttack() {
        if (aimDirection.sqrMagnitude < 0.001f) return;
        if (Time.time < NextAttack) return;
        if (!preset) return;
        NextAttack = Time.time + currentStats.attackRateInSeconds;

        Vector3 bulletScale = new Vector3(currentStats.attackSize, currentStats.attackSize, 1f);

        GameObject bulletObject = bulletFactory.Create(transform.position, Quaternion.identity, bulletScale);
        if (!bulletObject.TryGetComponent(out IBullet bullet)) return;
        
        Shoot(bullet, preset, aimDirection, currentStats.attackDamage, bulletStats);
    }

    private void Update() {
        HandleAttack();
    }

    private void Start() {
        NextAttack = Time.time + firstShotOffset;
    }
    
    protected override void OnAwake() {
        if (!preset) {
            Debug.LogWarning($"{name}: No {nameof(BulletPresetSO)} set!");
        }
    }
}
