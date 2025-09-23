using UnityEngine;

public interface IMovable {
    public void RequestMovement(Vector2 direction, float speed);
    /// <summary>
    /// Returns the normalized direction in which an object is moving
    /// </summary>
    public Vector2 GetMovementDirection();
}
