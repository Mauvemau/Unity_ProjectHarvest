using UnityEngine;

public interface IBulletStrategy {
    public void HandleMovement(Rigidbody2D rb, Vector2 direction, float speed);
}
