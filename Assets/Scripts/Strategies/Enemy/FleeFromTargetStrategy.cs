using UnityEngine;

public class FleeFromTargetStrategy : ICharacterBehaviourStrategy {
    public float GetComforRadius() {
        return 0f;
    }

    public float GetAwarenessRadius() {
        return 0f;
    }

    public void HandleMovement(Transform transform, Rigidbody2D rb, Transform targetTransform, float movementSpeed, Vector2 pushVelocity) {
        if (!targetTransform || !rb) return;
        Vector2 movementDirection = (transform.position - targetTransform.position).normalized;

        Vector2 move = movementDirection * (movementSpeed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + move;

        rb.MovePosition(newPosition);
    }
}
