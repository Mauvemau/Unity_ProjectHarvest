using UnityEngine;

[System.Serializable]
public class LinearShotStrategy : IBulletStrategy {
    public void HandleMovement(Rigidbody2D rb, Vector2 direction, float speed) {
        if (!rb) return;

        Vector2 move = direction.normalized * (speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + move * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
}
