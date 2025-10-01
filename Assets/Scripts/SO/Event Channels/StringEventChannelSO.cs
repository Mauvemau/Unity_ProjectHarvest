using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// String event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/String Channel")]
public class StringEventChannelSo : ScriptableObject {
    public UnityAction<string> onEventRaised;

    public void RaiseEvent(string value) {
        onEventRaised?.Invoke(value);
    }
}