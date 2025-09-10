using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour, IMovable {
    [Header("Physics Settings")]
    [SerializeField] private AnimationCurve gripCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float slipDuration = 0.5f;
    
    private Rigidbody2D _rb;
    private Vector2 _inputDir;
    private Vector2 _currentVelocity;
    private float _slipTimer;
    private float _currentSpeed;

    public void RequestPush(Vector2 direction, float force) {
        
    }

    public void RequestMovement(Vector2 direction, float speed) {
        _inputDir = direction.normalized;
        _currentSpeed = speed;
    }

    private void HandlePhysics() {
        if (_inputDir.sqrMagnitude > 0f) {
            _slipTimer = 0f;
            _currentVelocity = _inputDir * _currentSpeed;
        } 
        else {
            _slipTimer += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(_slipTimer / slipDuration);
            float factor = 1f - gripCurve.Evaluate(t);
            _currentVelocity *= factor;
        }

        _rb.linearVelocity = _currentVelocity;
    }
        
    private void FixedUpdate() {
        HandlePhysics();
    }
    
    private void Awake() {
        if (!TryGetComponent(out _rb)) return;
    }
}
