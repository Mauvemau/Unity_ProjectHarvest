using UnityEngine;

public class FollowTargetStrategy : ICharacterBehaviourStrategy {
    public float GetComforRadius() {
        return 0f;
    }

    public float GetAwarenessRadius() {
        return 0f;
    }

    public void HandleMovement(Transform transform, Rigidbody2D rb, Transform targetTransform, float movementSpeed, Vector2 pushVelocity) {
        if (!targetTransform || !rb) return;
        Vector2 movementDirection = (targetTransform.transform.position - transform.position).normalized;

        Vector2 move = movementDirection * (movementSpeed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + move + pushVelocity * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
}
