using UnityEngine;

[System.Serializable]
public class OrbitStrategy : IBulletStrategy {
    [SerializeField, Min(0.1f)] private float orbitRadius = 1f;
    [SerializeField] private bool stickToMass = false;

    private Transform _bulletTransform;
    private Vector2 _centerOfMass;
    private Rigidbody2D _rb;
    private float _movementSpeed;
    private float _currentAngle;
    private Transform _weaponTransform;

    public void Init(Transform bulletTransform, Rigidbody2D rb, Vector2 aimDirection, float movementSpeed, Transform weaponTransform = null) {
        _bulletTransform = bulletTransform;
        _weaponTransform = weaponTransform;
        _rb = rb;
        _movementSpeed = movementSpeed;
        _centerOfMass = weaponTransform ? weaponTransform.position : bulletTransform.position;

        _currentAngle = Mathf.Atan2(aimDirection.y, aimDirection.x);

        Vector2 offset = aimDirection.normalized * orbitRadius;
        _rb.position = (Vector2)_centerOfMass + offset;
    }

    public void HandleMovement() {
        if (!_rb) return;
        if(stickToMass && _weaponTransform) {
            _centerOfMass = _weaponTransform.position;
        }

        _currentAngle += _movementSpeed * Time.fixedDeltaTime / orbitRadius;

        Vector2 newOffset = new Vector2(Mathf.Cos(_currentAngle), Mathf.Sin(_currentAngle)) * orbitRadius;
        Vector2 newPosition = (Vector2)_centerOfMass + newOffset;

        _rb.MovePosition(newPosition);

        if (!_bulletTransform) return;
        Vector2 tangent = new Vector2(-Mathf.Sin(_currentAngle), Mathf.Cos(_currentAngle));
        float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
        _bulletTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public object Clone() => MemberwiseClone();
}
