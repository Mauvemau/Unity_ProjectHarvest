using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Menu event channel that can broadcast events to listeners
/// </summary>
[CreateAssetMenu(menuName = "Events/Menu Channel")]
public class MenuEventChannelSO : ScriptableObject {
    public UnityAction<Menu> OnEventRaised;

    public void RaiseEvent(Menu value) {
        OnEventRaised?.Invoke(value);
    }
}
