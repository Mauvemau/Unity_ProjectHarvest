using UnityEngine;

public interface IBulletStrategy {
    public void HandleMovement(Transform transform, Rigidbody2D rb, Vector2 direction, float speed);
}
