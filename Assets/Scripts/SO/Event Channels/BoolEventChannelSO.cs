using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bool event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Bool Channel")]
public class BoolEventChannelSO : ScriptableObject {
    public UnityAction<bool> onEventRaised;

    public void RaiseEvent(bool value) {
        onEventRaised?.Invoke(value);
    }
}
