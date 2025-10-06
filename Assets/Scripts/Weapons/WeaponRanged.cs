using UnityEngine;

public class WeaponRanged : Weapon {
    [Header("Factory Settings")]
    [SerializeField] private Factory bulletFactory;

    [Header("Bullet Settings")]
    [SerializeField] private BulletPresetSO preset;
    [SerializeField] private float bulletSpeed;

    private void HandleAttack() {
        if (aimDirection.sqrMagnitude < 0.001f) return;
        if (Time.time < nextAttack) return;
        nextAttack = Time.time + currentStats.attackRateInSeconds;

        Vector3 bulletScale = new Vector3(currentStats.attackSize, currentStats.attackSize, 1f);

        GameObject bulletObject = bulletFactory.Create(transform.position, Quaternion.identity, bulletScale);
        if (!bulletObject.TryGetComponent(out IBullet bullet)) return;

        bullet.Shoot(preset, aimDirection, targetLayer, currentStats.attackDamage, bulletSpeed);
    }

    private void Update() {
        HandleAttack();
    }

    protected override void OnAwake() {

    }
}
