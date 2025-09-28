using UnityEngine;

public class HealthBar : ProgressBar {
    [Header("Bar Specific Settings")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color emptyHealthColor = Color.red;

    protected override void OnValueUpdated() {
        if (!fillImage) return;
        fillImage.fillAmount = Mathf.Clamp01(currentValue / maxValue);
        UpdateHealthBarColor();
    }
    
    private void UpdateHealthBarColor() {
        if (!fillImage) return;
        fillImage.color = Color.Lerp(emptyHealthColor, fullHealthColor, fillImage.fillAmount);
    }
}
