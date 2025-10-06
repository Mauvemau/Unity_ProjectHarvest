using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Scanner : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private float scannerRadius = 5f;
    [SerializeField] private LayerMask scanLayer;

    private readonly HashSet<Collider2D> _currentOverlaps = new HashSet<Collider2D>();

    public GameObject GetClosest(Vector2 position) {
        GameObject closest = null;
        int overlapsCount = _currentOverlaps.Count;
        if (overlapsCount <= 0) return closest;

        float closestDist = float.MaxValue;

        foreach (Collider2D collider in _currentOverlaps) {
            if (!collider || !collider.gameObject.activeInHierarchy)
                continue;

            float dist = ((Vector2)collider.transform.position - position).sqrMagnitude;
            if (dist < closestDist) {
                closestDist = dist;
                closest = collider.gameObject;
            }
        }

        return closest;
    }

    public GameObject[] GetAll() {
        int count = 0;
        GameObject[] result = new GameObject[_currentOverlaps.Count];

        foreach (Collider2D collider in _currentOverlaps) {
            if (collider && collider.gameObject.activeInHierarchy) {
                result[count++] = collider.gameObject;
            }
        }
        if (count < result.Length) {
            System.Array.Resize(ref result, count);
        }

        return result;
    }

    private void SetUpCollider() {
        if (!TryGetComponent(out CircleCollider2D col)) {
            Debug.LogError($"{name}: missing required component {nameof(CircleCollider2D)}");
            return;
        }
        col.isTrigger = true;
        col.radius = scannerRadius;

        int inverted = ~0 & ~scanLayer;
        col.includeLayers = 0;
        col.excludeLayers = inverted;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        _currentOverlaps.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _currentOverlaps.Remove(collision);
    }

    private void Awake() {
        SetUpCollider();
    }

    private void OnValidate() {
        SetUpCollider();
    }
}
