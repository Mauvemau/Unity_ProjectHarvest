using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class LinearShotStrategy : IBulletStrategy {
    public void HandleMovement(Transform transform, Rigidbody2D rb, Vector2 direction, float speed) {
        if (!rb) return;
        if (direction.sqrMagnitude < 0.001f) return;

        Vector2 move = direction.normalized * (speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + move * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
