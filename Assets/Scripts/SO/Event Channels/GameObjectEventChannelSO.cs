using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GameObject event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/GameObject Channel")]
public class GameObjectEventChannelSo : ScriptableObject {
    public UnityAction<GameObject> OnEventRaised;

    public void RaiseEvent(GameObject value) {
        OnEventRaised?.Invoke(value);
    }
}
