using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GameObject event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/GameObject Channel")]
public class GameObjectEventChannelSo : ScriptableObject {
    public UnityAction<GameObject> onEventRaised;

    public void RaiseEvent(GameObject value) {
        onEventRaised?.Invoke(value);
    }
}
