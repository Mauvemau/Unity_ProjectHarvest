using UnityEngine;

public class Hud : MonoBehaviour {
    [Header("Settings")] 
    [SerializeField] private bool startDisabled = true;
    
    [Header("Event Listeners")]
    [SerializeField] private BoolEventChannelSO onRequestEnable;

    private void SetEnabled(bool setEnabled) {
        gameObject.SetActive(setEnabled);
    }

    private void Awake() {
        if (onRequestEnable) {
            onRequestEnable.OnEventRaised += SetEnabled;
        }
        
        if (!startDisabled) return;
        SetEnabled(false);
    }

    private void OnDestroy() {
        if (onRequestEnable) {
            onRequestEnable.OnEventRaised -= SetEnabled;
        }
    }
}
