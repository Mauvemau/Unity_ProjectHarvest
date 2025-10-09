using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// String event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/String Channel")]
public class StringEventChannelSo : ScriptableObject {
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value) {
        OnEventRaised?.Invoke(value);
    }
}