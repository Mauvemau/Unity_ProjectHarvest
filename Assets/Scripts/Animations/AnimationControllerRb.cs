using UnityEngine;

/// <summary>
/// Defines animation statuses based on the motion of a rigidbody
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class AnimationControllerRb : MonoBehaviour {
    [SerializeField] private SpriteRenderer spriteRendererReference;

    private Rigidbody2D _rb;

    private void Update() {
        if (!_rb) return;
        if (spriteRendererReference) {
            if (_rb.linearVelocity.x > 0) {
                spriteRendererReference.flipX = false;
            }else if (_rb.linearVelocity.x < 0) {
                spriteRendererReference.flipX = true;
            }
        }
    }

    private void Awake() {
        if (!TryGetComponent(out _rb)) {
            Debug.LogError($"{name}: missing required component {nameof(CircleCollider2D)}");
        }
    }
}
