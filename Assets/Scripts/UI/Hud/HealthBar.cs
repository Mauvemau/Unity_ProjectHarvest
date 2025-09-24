using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color emptyHealthColor = Color.red;

    private float _maxValue;
    
    public void SetMaxValue(float amount) {
        _maxValue = amount;
        SetCurrentValue(amount);
    }

    public void SetCurrentValue(float amount) {
        if (!fillImage) return;
        fillImage.fillAmount = Mathf.Clamp01(amount / _maxValue);
        UpdateHealthBarColor();
    }

    private void UpdateHealthBarColor() {
        fillImage.color = Color.Lerp(emptyHealthColor, fullHealthColor, fillImage.fillAmount);
    }
    
    private void Awake() {
        fillImage.type = Image.Type.Filled;
    }
}
