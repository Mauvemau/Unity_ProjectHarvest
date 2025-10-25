using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonIgnoreAlphaHitbox : MonoBehaviour {
    [SerializeField] private float minimumAlphaThreshHold = 0.5f;
    private Image _imageReference;

    private void Awake() {
        if (!TryGetComponent(out _imageReference)) {
            Debug.LogError($"{name}: missing required component {nameof(Image)}!");
            return;
        }
        _imageReference.alphaHitTestMinimumThreshold = minimumAlphaThreshHold;
    }
}
