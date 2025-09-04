using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour, IMovable {

    private Rigidbody2D _rb;

    public void RequestPush(Vector2 direction, float force) {

    }

    public void RequestMovement(Vector2 direction, float speed) {

    }

    private void FixedUpdate() {
        
    }

    private void Awake() {
        if (!TryGetComponent(out _rb)) return;
    }
}
