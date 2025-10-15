using UnityEngine;

public class RotationConstraintApplier : MonoBehaviour {
    private Quaternion _initialRotation;

    private void Awake() {
        _initialRotation = transform.rotation;
    }

    private void LateUpdate() {
        transform.rotation = _initialRotation;
    }
}