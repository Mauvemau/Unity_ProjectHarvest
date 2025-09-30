using UnityEngine;

public class HealthBar : ProgressBar {
    [Header("Bar Specific Settings")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color emptyHealthColor = Color.red;

    private void UpdateHealthBarColor() {
        if (!fillImage) return;
        fillImage.color = Color.Lerp(emptyHealthColor, fullHealthColor, fillImage.fillAmount);
    }
    
    protected override void OnValueUpdated() {
        if (!fillImage) return;
        fillImage.fillAmount = Mathf.Clamp01(currentValue / maxValue);
        
        if (percentageText) {
            percentageText.text = $"{Mathf.Round((currentValue / maxValue) * 100f)}%";
        }
        
        UpdateHealthBarColor();
    }
}
