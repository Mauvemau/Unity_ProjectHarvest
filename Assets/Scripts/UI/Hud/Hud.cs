using UnityEngine;

public class Hud : MonoBehaviour {
    [Header("Event Listeners")]
    [SerializeField] private BoolEventChannelSO onRequestEnable;

    private void SetEnabled(bool enabled) {
        gameObject.SetActive(enabled);
    }

    private void Awake() {
        if (onRequestEnable) {
            onRequestEnable.OnEventRaised += SetEnabled;
        }
    }

    private void OnDestroy() {
        if (onRequestEnable) {
            onRequestEnable.OnEventRaised -= SetEnabled;
        }
    }
}
