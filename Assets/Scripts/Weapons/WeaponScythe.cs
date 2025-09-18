using UnityEngine;

public class WeaponScythe : Weapon {
    [Header("Weapon Specific Stats")]
    [SerializeField, Range(10f, 360f)] private float attackAngle = 90f;
    [SerializeField] private float pushForce = 12f;

    [Header("Visual Settings")]
    [SerializeField, Min(12)] private int arcSegments = 40;
    [SerializeField] private AnimationCurve lineWidth = AnimationCurve.Linear(0f, .1f, 1f, .1f);

    private LineRenderer _lineRenderer;

    private void DrawAttackRange() {
        int segments = arcSegments;
        _lineRenderer.positionCount = segments + 1;

        for (int i = 0; i <= segments; i++) {
            float angle = (360f / segments) * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 point = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * currentStats.attackSize;
            _lineRenderer.SetPosition(i, point);
        }
    }

    private void DrawAttackAngle() {
        float aimAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        float startAngle = aimAngle - attackAngle / 2f;


    }

    private void DrawAttackArea() {
        if (!_lineRenderer) return;
        _lineRenderer.widthCurve = lineWidth;
        DrawAttackRange();
        DrawAttackAngle();
    }

    private void Attack() {

    }

    private void Update() {
        DrawAttackArea();

        Attack();
    }

    protected override void OnAwake() {
        if (!TryGetComponent(out _lineRenderer)) {
            Debug.LogWarning($"{name}: missing reference \"{nameof(_lineRenderer)}\", the attack area will not be drawn.");
        }
    }
}
