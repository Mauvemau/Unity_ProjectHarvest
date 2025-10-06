using UnityEngine;

public interface IBullet {
    public void Shoot(BulletPresetSO presetToSet, Vector2 direction, LayerMask targetLayer, float damage, float speed);
}
