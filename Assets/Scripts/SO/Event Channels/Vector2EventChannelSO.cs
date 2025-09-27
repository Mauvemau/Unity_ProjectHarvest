using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Vector2 event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Vector2 Channel")]
public class Vector2EventChannelSO : ScriptableObject {
    public UnityAction<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 value) {
        OnEventRaised?.Invoke(value);
    }
}
