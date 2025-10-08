using UnityEngine;

/// <summary>
/// Defines animation statuses based on the motion of a IMovable interface
/// </summary>
[RequireComponent(typeof(IFacingDirection))]
public class AnimationControllerMovement : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private SpriteRenderer rendererReference;
    
    private IFacingDirection _facingDirection;

    private bool IsGamePaused() {
        return Time.timeScale <= 0.0f;
    }
    
    private void Awake() {
        if (!TryGetComponent(out _facingDirection)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_facingDirection)}\"");
        }
    }
    
    private void Update() {
        if (_facingDirection == null) return;
        if (IsGamePaused()) return;
        if (!rendererReference) return;
        if (_facingDirection.GetFacingDirection().x > 0) {
            rendererReference.flipX = false;
        }
        else if (_facingDirection.GetFacingDirection().x < 0) {
            rendererReference.flipX = true;
        }
    }
}
