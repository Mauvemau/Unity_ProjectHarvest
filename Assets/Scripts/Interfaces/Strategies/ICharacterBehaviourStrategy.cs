using UnityEngine;

public interface ICharacterBehaviourStrategy {
    /// <summary>
    /// Returns where the entity should be facing during each action
    /// </summary>
    public Vector2 GetDirectionVector();
    /// <summary>
    /// Used to draw gizmos for the AI.
    /// </summary>
    public float GetComforRadius();
    /// <summary>
    /// Used to draw gizmos for the AI.
    /// </summary>
    /// <returns></returns>
    public float GetAwarenessRadius();
    /// <summary>
    /// Handles the movement behaviour of the AI, put this inside a FixedUpdate block.
    /// </summary>
    public void HandleMovement(Transform transform, Rigidbody2D rb, Transform targetTransform, float movementSpeed, Vector2 pushVelocity);
}
