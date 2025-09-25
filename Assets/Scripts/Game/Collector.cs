using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Collector : MonoBehaviour {
    [Header("Settings")]
    [SerializeField, Min(0)] private float collectRadius = 3f;
    [SerializeField] private LayerMask collectableLayer;

    [Header("Visual Settings")]
    [SerializeField] private bool drawRadiusGizmo = true;
    [SerializeField] private Color gizmoColor = Color.magenta;

    private void HandleTrigger(Collider2D collision) {
        if (!collision.TryGetComponent(out ICollectable collectable)) return;
        collectable.Collect(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        HandleTrigger(collision);    
    }

    private void OnDrawGizmos() {
        if (!drawRadiusGizmo) return;
        UnityEditor.Handles.color = gizmoColor;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, collectRadius);
    }

    private void OnValidate() {
        if (!TryGetComponent(out CircleCollider2D collider)) {
            Debug.LogError($"{name}: missing reference \"{nameof(collider)}\"");
            return;
        }
        collider.isTrigger = true;
        collider.radius = collectRadius;
    }
}
