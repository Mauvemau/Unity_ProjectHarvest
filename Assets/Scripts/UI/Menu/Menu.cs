using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Menu: MonoBehaviour, IMenu {
    [Header("Button Settings")]
    [SerializeField] private Button initialButton;

    [Header("Event Listeners")] 
    [SerializeField] public VoidEventChannelSO onRequestOpenRemotely;
    
    [Header("Event Invokers")] 
    [Tooltip("Do not set if the menu is not supposed to pause the game")]
    [SerializeField] private BoolEventChannelSO onOpenMenuGamePauseChannel;
    [Tooltip("Do not set if the menu is not supposed to hide the game's hud")]
    [SerializeField] private BoolEventChannelSO onToggleHudChannel;

    public Button GetInitialButton() => initialButton;

    public void Toggle(bool toggle) {
        if (onOpenMenuGamePauseChannel) {
            onOpenMenuGamePauseChannel.RaiseEvent(toggle);
        }
        if (onToggleHudChannel) {
            onToggleHudChannel.RaiseEvent(!toggle);
        }
        this.gameObject.SetActive(toggle);
    }

    public void Open() {
        Toggle(true);
    }

    public void Close() {
        Toggle(false);
    }
}
