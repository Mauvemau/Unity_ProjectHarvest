using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Void event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Void Channel")]
public class VoidEventChannelSO : ScriptableObject {
    public UnityAction onEventRaised;

    public void RaiseEvent() {
        onEventRaised?.Invoke();
    }
}
