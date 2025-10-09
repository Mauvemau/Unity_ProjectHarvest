using UnityEngine;

public class WeaponRanged : Weapon {
    [Header("Factory Settings")]
    [SerializeField] private Factory bulletFactory;

    [Header("Bullet Settings")]
    [SerializeField] private BulletPresetSO preset;
    [SerializeField] private BulletStats bulletStats;

    private void HandleAttack() {
        if (aimDirection.sqrMagnitude < 0.001f) return;
        if (Time.time < NextAttack) return;
        NextAttack = Time.time + currentStats.attackRateInSeconds;

        Vector3 bulletScale = new Vector3(currentStats.attackSize, currentStats.attackSize, 1f);

        GameObject bulletObject = bulletFactory.Create(transform.position, Quaternion.identity, bulletScale);
        if (!bulletObject.TryGetComponent(out IBullet bullet)) return;

        bullet.Shoot(preset, aimDirection, targetLayer, currentStats.attackDamage, bulletStats, gameObject.transform);
    }

    private void Update() {
        HandleAttack();
    }

    protected override void OnAwake() {

    }
}
