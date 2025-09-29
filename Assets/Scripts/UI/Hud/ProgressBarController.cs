using UnityEngine;

[System.Serializable]
public class ProgressBarController {
    [SerializeField] private FloatEventChannelSO onMaxValueUpdated;
    [SerializeField] private FloatEventChannelSO onCurrentValueUpdated;
    
    public void UpdateMaxValue(float maxValue) {
        if (onMaxValueUpdated) {
            onMaxValueUpdated.RaiseEvent(maxValue);
        }
    }
    
    public void UpdateCurrentValue(float currentValue) {
        if (onCurrentValueUpdated) {
            onCurrentValueUpdated.RaiseEvent(currentValue);
        }
    }
    
    public void UpdateValues(float currentValue, float maxValue) {
        UpdateMaxValue(maxValue);
        UpdateCurrentValue(currentValue);
    }
}
