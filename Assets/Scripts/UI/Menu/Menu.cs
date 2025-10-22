using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu: MonoBehaviour, IMenu {
    [Header("References")]
    [SerializeField] private EventSystem eventSystem;
    
    [Header("Button Settings")]
    [SerializeField] private Button initialButton;

    [Header("Event Listeners")] 
    [SerializeField] public VoidEventChannelSO onRequestOpenRemotely;
    
    [Header("Event Invokers")] 
    [Tooltip("Do not set if the menu is not supposed to pause the game")]
    [SerializeField] private BoolEventChannelSO onOpenMenuGamePauseChannel;
    [Tooltip("Do not set if the menu is not supposed to hide the game's hud")]
    [SerializeField] private BoolEventChannelSO onToggleHudChannel;
    
    private GameObject _lastSelected;
    
    public Button InitialButton() => initialButton;

    public void Toggle(bool toggle) {
        if (onOpenMenuGamePauseChannel) {
            onOpenMenuGamePauseChannel.RaiseEvent(toggle);
        }
        if (onToggleHudChannel) {
            onToggleHudChannel.RaiseEvent(!toggle);
        }
        gameObject.SetActive(toggle);
        
        if (!initialButton) return;
        eventSystem.SetSelectedGameObject(initialButton.gameObject);
    }
    
    public void Open() {
        Toggle(true);
    }

    public void Close() {
        Toggle(false);
    }
    
    private void Update() {
        if (!eventSystem) return;

        GameObject current = eventSystem.currentSelectedGameObject;

        if (current && current != _lastSelected) {
            _lastSelected = current;
        }
        if (!current && _lastSelected) {
            eventSystem.SetSelectedGameObject(_lastSelected);
        }
    }
}
