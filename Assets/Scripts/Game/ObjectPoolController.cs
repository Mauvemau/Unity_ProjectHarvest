using UnityEngine;

[System.Serializable]
public class ObjectPoolController {
    [Header("Event Invokers")]
    [SerializeField] private Vector2EventChannelSO onRequestPoolChannel;
    [SerializeField] private VoidEventChannelSO onReturnPoolChannel;

    public void PerformPoolRequest(Vector2 position) {
        if (onRequestPoolChannel) {
            onRequestPoolChannel.RaiseEvent(position);
        }
    }

    public void PerformPoolReturn() {
        if (onReturnPoolChannel) {
            onReturnPoolChannel.RaiseEvent();
        }
    }
}
