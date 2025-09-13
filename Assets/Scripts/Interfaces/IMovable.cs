using UnityEngine;

public interface IMovable {
    public void RequestMovement(Vector2 direction, float speed);
}
