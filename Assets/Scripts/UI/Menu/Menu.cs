using UnityEngine;
using UnityEngine.UI;

public class Menu: MonoBehaviour, IMenu {
    [SerializeField] private Button initialButton;

    public Button GetInitialButton() => initialButton;

    public void Toggle(bool toggle) {
        this.gameObject.SetActive(toggle);
    }

    public void Open() {
        Toggle(true);
    }

    public void Close() {
        Toggle(false);
    }
}
