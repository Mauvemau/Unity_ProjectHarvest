using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour {
    [Header("Optional References")]
    [SerializeField] protected Image fillImage;
    [SerializeField] protected TMP_Text percentageText;
    
    [Header("Event Listeners")]
    [SerializeField] private FloatEventChannelSO setMaxValueListener;
    [SerializeField] private FloatEventChannelSO setCurrentValueListener;

    protected float maxValue;
    protected float currentValue;

    protected virtual void OnValueUpdated() {
        if (!fillImage) return;
        fillImage.fillAmount = Mathf.Clamp01(currentValue / maxValue);
        
        if (percentageText) {
            percentageText.text = $"{Mathf.Round((currentValue / maxValue) * 100f)}%";
        }
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

        if (amount < 0) {
            amount = 0;
        }

        if (amount > maxValue) {
            amount = maxValue;
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
    
    private void OnEnable() {
        if (setMaxValueListener) {
            setMaxValueListener.onEventRaised += SetMaxValue;
        }
        if (setCurrentValueListener) {
            setCurrentValueListener.onEventRaised += SetCurrentValue;
        }
    }

    private void OnDisable() {
        if (setMaxValueListener) {
            setMaxValueListener.onEventRaised -= SetMaxValue;
        }
        if (setCurrentValueListener) {
            setCurrentValueListener.onEventRaised -= SetCurrentValue;
        }
    }
}
