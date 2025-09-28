using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class ProgressBarController : MonoBehaviour {
    [Header("Event Listeners")]
    [SerializeField] private FloatEventChannelSO setMaxValueListener;
    [SerializeField] private FloatEventChannelSO setCurrentValueListener;

    private ProgressBar _progressBar;
    private void OnEnable() {
        if (!TryGetComponent(out _progressBar)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_progressBar)}\"");
            return;
        }
        
        if (setMaxValueListener) {
            setMaxValueListener.OnEventRaised += _progressBar.SetMaxValue;
        }
        if (setCurrentValueListener) {
            setCurrentValueListener.OnEventRaised += _progressBar.SetCurrentValue;
        }
    }

    private void OnDisable() {
        if (!TryGetComponent(out _progressBar)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_progressBar)}\"");
            return;
        }
        
        if (setMaxValueListener) {
            setMaxValueListener.OnEventRaised -= _progressBar.SetMaxValue;
        }
        if (setCurrentValueListener) {
            setCurrentValueListener.OnEventRaised -= _progressBar.SetCurrentValue;
        }
    }
}
