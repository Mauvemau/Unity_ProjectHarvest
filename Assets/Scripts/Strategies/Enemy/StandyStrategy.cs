using UnityEngine;

public class StandyStrategy : ICharacterBehaviourStrategy {
    public float GetComforRadius() {
        return 0f;
    }

    public float GetAwarenessRadius() {
        return 0f;
    }

    public void HandleMovement(Transform transform, Rigidbody2D rb, Transform targetTransform, float movementSpeed, Vector2 pushVelocity) {
        return;
    }
}
