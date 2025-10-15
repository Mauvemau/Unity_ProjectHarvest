using UnityEngine;

public class RelativePositionConstraint : MonoBehaviour {
    [SerializeField] private Transform parent;

    private Vector3 _offsetFromParent;

    private void Awake() {
        if (!parent) {
            parent = transform.parent;
        }

        _offsetFromParent = transform.position - parent.position;
    }

    private void LateUpdate() {
        transform.position = parent.position + _offsetFromParent;
    }
}