using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    [Header("Optional References")]
    [SerializeField] protected Image fillImage;

    protected float maxValue;
    protected float currentValue;

    protected virtual void OnValueUpdated() {
        if (!fillImage) return;
        fillImage.fillAmount = Mathf.Clamp01(currentValue / maxValue);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void SetMaxValue(float amount) {
        if (amount <= 0) {
            Debug.LogWarning($"{name}: Trying to set invalid max value: {amount}");
            return;
        }
        maxValue = amount;
        SetCurrentValue(amount);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void SetCurrentValue(float amount) {
        if (maxValue <= 0) {
            Debug.LogWarning($"{name}: Trying to update value of progress bar with unset max value!");
            return;
        }

        if (currentValue < 0) {
            currentValue = 0;
        }

        if (currentValue > maxValue) {
            currentValue = maxValue;
        }
        
        currentValue = amount;
        OnValueUpdated();
    }

    protected virtual void OnValidated() { }
    
    private void OnValidate() {
        if(fillImage)
            fillImage.type = Image.Type.Filled;
        OnValidated();
    }
}
