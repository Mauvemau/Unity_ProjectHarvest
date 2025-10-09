using System;
using UnityEngine;

public interface IBulletStrategy : ICloneable {
    public void Init(Transform transform, Rigidbody2D rb, Vector2 aimDirection, float movementSpeed);
    public void HandleMovement();
}
