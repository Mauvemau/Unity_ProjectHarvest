using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Audio event channel that can broadcast wwise audio events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Wwise audio event Channel")]
public class AudioEventChannelSO : ScriptableObject {
    public UnityAction<AK.Wwise.Event> OnEventRaised;

    public void RaiseEvent(AK.Wwise.Event audioEvent) {
        OnEventRaised?.Invoke(audioEvent);
    }
}