using UnityEngine;

/// <summary>
/// Defines animation statuses based on the motion of a IMovable interface
/// </summary>
[RequireComponent(typeof(IMovable))]
public class AnimationControllerMovement : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private SpriteRenderer rendererReference;
    
    private IMovable _movable;

    private void Awake() {
        if (!TryGetComponent(out _movable)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_movable)}\"");
        }
    }
    
    private void Update() {
        if (_movable == null) return;
        if (rendererReference) {
            if (_movable.GetMovementDirection().x > 0) {
                rendererReference.flipX = false;
            }
            else if (_movable.GetMovementDirection().x < 0) {
                rendererReference.flipX = true;
            }
        }
    }
}
