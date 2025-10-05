using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour {
    [Header("Behaviour Settings")]
    [SerializeReference, SubclassSelector] private IBulletStrategy behaviourHandler = new LinearShotStrategy();

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private Rigidbody2D _rb;
    private Vector2 _aimDirection;
    private float _damage = 0f;
    private float _speed = 50f;
    [SerializeField] private bool _shot = false;

    private readonly HashSet<Collider2D> _currentOverlaps = new HashSet<Collider2D>();

    [ContextMenu("Debug - Shoot")]
    private void ShootDebug() {
        Shoot(Vector2.right, 0, _damage, 0f, _speed);
    }

    public void Shoot(Vector2 direction, LayerMask targetLayer, float damage, float bulletRadius, float speed) {
        SetUpCollision(targetLayer, bulletRadius);
        _aimDirection = direction;
        _damage = damage;
        _speed = speed;

        _shot = true;
    }

    private void SetUpCollision(LayerMask targetLayer, float bulletRadius) {
        if (!_collider) return;

        int inverted = ~0 & ~targetLayer;
        _collider.includeLayers = 0;
        _collider.excludeLayers = inverted;

        if (bulletRadius <= 0) return;
        float baseRadius = _collider.radius;
        _collider.radius = bulletRadius;

        if (!_spriteRenderer) return;
        float scale = bulletRadius / baseRadius;
        _spriteRenderer.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private void HandleCollision(Collider2D other) {
        if (other.TryGetComponent(out IDamageable damageable)) {
            damageable.TakeDamage(_damage);
        }
        gameObject.SetActive(false); // If layered correctly it will disappear upon hitting walls
    }

    private void FixedUpdate() {
        if (!_shot && behaviourHandler == null) return;
        behaviourHandler.HandleMovement(_rb, _aimDirection, _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!_shot) return;
        if (_currentOverlaps.Add(collision)) { // Will return false if target is already inside
            HandleCollision(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!_shot) return;
        _currentOverlaps.Remove(collision);
    }

    private void Awake() {
        if (!TryGetComponent(out _collider)) {
            Debug.LogError($"{name}: missing required component {nameof(CircleCollider2D)}");
        }
        if (!TryGetComponent(out _rb)) {
            Debug.LogError($"{name}: missing required component {nameof(CircleCollider2D)}");
        }
        if (!TryGetComponent(out _spriteRenderer)) {
            Debug.LogWarning($"{name}: missing optional component {nameof(CircleCollider2D)}");
        }
    }

    private void OnEnable() {
        _shot = false;
        _currentOverlaps.Clear();
        if (_collider) {
            _collider.isTrigger = true;
        }
        if (_rb) {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}
