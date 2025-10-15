using UnityEngine;

[System.Serializable]
public class OrbitStrategy : IBulletStrategy {
    [SerializeField, Min(0.1f)] private float initialOrbitRadius = 1f;
    [SerializeField] private float orbitExpansionSpeed = 0f; // > 0 expand outwards, < 0 expand inwards
    [SerializeField] private float bulletSpinSpeed = 0f; // 0 = face movement direction > 0 spin around
    [SerializeField] private bool clockWise = false;
    [SerializeField] private bool stickToMass = false;

    private Transform _bulletTransform;
    private Vector2 _centerOfMass;
    private Rigidbody2D _rb;
    private float _movementSpeed;
    private float _currentAngle;
    private Transform _weaponTransform;
    private float _currentOrbitRadius;

    public void Init(Transform bulletTransform, Rigidbody2D rb, Vector2 aimDirection, float movementSpeed, Transform weaponTransform = null) {
        _bulletTransform = bulletTransform;
        _weaponTransform = weaponTransform;
        _rb = rb;
        _movementSpeed = movementSpeed;
        _centerOfMass = weaponTransform ? weaponTransform.position : bulletTransform.position;
        _currentOrbitRadius = initialOrbitRadius;

        _currentAngle = Mathf.Atan2(aimDirection.y, aimDirection.x);

        Vector2 offset = aimDirection.normalized * _currentOrbitRadius;
        _rb.position = (Vector2)_centerOfMass + offset;
    }

    public void HandleMovement() {
        if (!_rb) return;
        if(stickToMass && _weaponTransform) {
            _centerOfMass = _weaponTransform.position;
        }

        _currentAngle += (clockWise ? _movementSpeed : -_movementSpeed) * Time.fixedDeltaTime / _currentOrbitRadius;
        _currentOrbitRadius += orbitExpansionSpeed * Time.fixedDeltaTime;

        Vector2 newOffset = new Vector2(Mathf.Cos(_currentAngle), Mathf.Sin(_currentAngle)) * _currentOrbitRadius;
        Vector2 newPosition = (Vector2)_centerOfMass + newOffset;

        _rb.MovePosition(newPosition);

        if (!_bulletTransform) return;
        Vector2 tangent = new Vector2(-Mathf.Sin(_currentAngle), Mathf.Cos(_currentAngle));
        float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
        if (bulletSpinSpeed < 0.01f) {
            _bulletTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else {
            float nextAngle = _bulletTransform.rotation.eulerAngles.z + bulletSpinSpeed * Time.fixedDeltaTime;
            _bulletTransform.rotation = Quaternion.Euler(0f, 0f, nextAngle);
        }
    }

    public object Clone() => MemberwiseClone();
}
