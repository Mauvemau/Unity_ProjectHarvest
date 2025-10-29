using UnityEngine;

/// <summary>
/// Defines animation statuses based on the motion of a IMovable interface
/// </summary>
[RequireComponent(typeof(IFacingDirection))]
public class AnimationControllerMovement : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private SpriteRenderer rendererReference;
    [SerializeField] private Animator animatorReference;
    
    private IFacingDirection _facingDirection;
    private IMovable _movable;

    private static readonly int Moving = Animator.StringToHash("moving");
    
    private bool IsGamePaused() {
        return Time.timeScale <= 0.0f;
    }

    private void UpdateAnimator() {
        if (_movable == null) return;
        if (!animatorReference) return;
        if (IsGamePaused()) return;
        
        animatorReference.SetBool(Moving, _movable.GetMovementDirection().magnitude > 0.0f);
    }
    
    private void UpdateMirroring() {
        if (_facingDirection == null) return;
        if (!rendererReference) return;
        if (IsGamePaused()) return;
        
        if (_facingDirection.GetFacingDirection().x > 0) {
            rendererReference.flipX = false;
        }
        else if (_facingDirection.GetFacingDirection().x < 0) {
            rendererReference.flipX = true;
        }
    }
    
    private void Awake() {
        if (!TryGetComponent(out _facingDirection)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_facingDirection)}\"");
        }
        _movable = GetComponent<IMovable>();
    }
    
    private void Update() {
        UpdateMirroring();
        UpdateAnimator();
    }
}
