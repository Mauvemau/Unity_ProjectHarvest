using UnityEngine;

[System.Serializable]
public class FleeFromTargetStrategy : ICharacterBehaviourStrategy {
    private Vector2 _movementDirection = Vector2.zero;

    public Vector2 GetDirectionVector() => _movementDirection;

    public float GetComforRadius() => 0f;
    public float GetAwarenessRadius() => 0f;

    public void HandleMovement(Transform transform, Rigidbody2D rb, Transform targetTransform, float movementSpeed, Vector2 pushVelocity) {
        if (!targetTransform || !rb) return;
        _movementDirection = (transform.position - targetTransform.position).normalized;

        Vector2 move = _movementDirection * (movementSpeed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + move;

        rb.MovePosition(newPosition);
    }
}
