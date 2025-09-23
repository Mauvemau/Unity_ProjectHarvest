using UnityEngine;

/// <summary>
/// Defines animation statuses based on the motion of a rigidbody
/// </summary>
public class AnimationControllerRb : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbodyReference;
    [SerializeField] private SpriteRenderer spriteRendererReference;

    private void Update() {
        if (!rigidbodyReference) return;
        if (spriteRendererReference) {
            if (rigidbodyReference.linearVelocity.x > 0) {
                spriteRendererReference.flipX = false;
            }else if (rigidbodyReference.linearVelocity.x < 0) {
                spriteRendererReference.flipX = true;
            }
        }
    }
}
