using UnityEngine;

public class WeaponTickingRadius : Weapon {
    [Header("References")] 
    [SerializeField] private Scanner scannerReference;

    [Header("Weapon Specific Settings")] 
    [SerializeField] private float pushForce = 0f;
    [SerializeField] private bool ignoreParent = true;
    
    [Header("Visual Settings")] 
    [SerializeField, Range(12, 32)] private int gizmoAccuracy = 12;
    [SerializeField] private Color radiusGizmoColor = Color.green;

    [SerializeField, ReadOnly] private GameObject[] currentOverlaps;
    
    private void HandleAttack() {
        if (Time.time < NextAttack) return;
        NextAttack = Time.time + currentStats.attackRateInSeconds;

        scannerReference.SetRadius(BaseStats.attackSize);
        
        currentOverlaps = scannerReference.GetAll();
        foreach (GameObject other in currentOverlaps) {
            if (other.transform == transform.parent && ignoreParent) continue;
            if (!other.TryGetComponent(out IDamageable damageable)) continue;
            damageable.TakeDamage(BaseStats.attackDamage);
            
            if (!other.TryGetComponent(out IPushable pushable)) continue;
            if (pushForce < 1) continue;
            Vector2 pushDirection = other.transform.position - transform.position;
            pushable.RequestPush(pushDirection, pushForce);
        }
    }
    
    private void Update() {
        HandleAttack();
    }

    private void OnDrawGizmos() {
        Gizmos.color = radiusGizmoColor;
        
        Vector3 prevPoint = Vector3.zero;
        for (int i = 0; i <= gizmoAccuracy; i++) {
            float angle = i * Mathf.PI * 2f / gizmoAccuracy;
            Vector3 newPoint = new Vector3(
                transform.position.x + Mathf.Cos(angle) * BaseStats.attackSize,
                transform.position.y + Mathf.Sin(angle) * BaseStats.attackSize,
                0f
            );

            if (i > 0) {
                Gizmos.DrawLine(prevPoint, newPoint);
            }

            prevPoint = newPoint;
        }
    }
}

