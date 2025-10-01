using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Float event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Float Channel")]
public class FloatEventChannelSO : ScriptableObject {
    public UnityAction<float> onEventRaised;

    public void RaiseEvent(float value) {
        onEventRaised?.Invoke(value);
    }
}
