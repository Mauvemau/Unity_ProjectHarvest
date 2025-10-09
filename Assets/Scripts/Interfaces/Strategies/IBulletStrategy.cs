using System;
using UnityEngine;

public interface IBulletStrategy : ICloneable {
    public void Init(Transform bulletTransform, Rigidbody2D rb, Vector2 aimDirection, float movementSpeed, Transform weaponTransform = null);
    public void HandleMovement();
}
