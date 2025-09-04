using UnityEngine;

public interface IMovable {
    public void RequestPush(Vector2 direction, float force);
    public void RequestMovement(Vector2 direction, float speed);
}
