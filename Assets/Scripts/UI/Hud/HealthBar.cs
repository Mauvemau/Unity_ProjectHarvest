using UnityEngine;

public class HealthBar : ProgressBar {
    [Header("Bar Specific Settings")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color midHealthColor = Color.yellow;
    [SerializeField] private Color emptyHealthColor = Color.red;
    [Tooltip(
    "Keeps the bar fully Red below this fraction and fully Green above (1 - this fraction). " +
    "Example: 0.1 = 0–10% stays Red, 90–100% stays Green, middle still blends Red→Yellow→Green.")]
    [SerializeField, Range(0f, 0.45f)] private float extremeOffset = 0.1f;

    private void UpdateHealthBarColor() {
        if (!fillImage) return;

        float t = fillImage.fillAmount;
        
        float lowerBound = extremeOffset;
        float upperBound = 1f - extremeOffset;

        if (t <= lowerBound) {
            fillImage.color = emptyHealthColor;
        }
        else if (t >= upperBound) {
            fillImage.color = fullHealthColor;
        }
        else if (t < 0.5f) {
            float lerp = Mathf.InverseLerp(lowerBound, 0.5f, t);
            fillImage.color = Color.Lerp(emptyHealthColor, midHealthColor, lerp);
        }
        else {
            float lerp = Mathf.InverseLerp(0.5f, upperBound, t);
            fillImage.color = Color.Lerp(midHealthColor, fullHealthColor, lerp);
        }
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
