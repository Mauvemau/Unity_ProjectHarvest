using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Vector2 event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Vector2 Channel")]
public class Vector2EventChannelSo : ScriptableObject {
    public UnityAction<Vector2> onEventRaised;

    public void RaiseEvent(Vector2 value) {
        onEventRaised?.Invoke(value);
    }
}
