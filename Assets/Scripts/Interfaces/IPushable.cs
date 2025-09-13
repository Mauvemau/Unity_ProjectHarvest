using UnityEngine;

public interface IPushable {
    public void RequestPush(Vector2 direction, float force);
}
