using UnityEngine.UI;

public interface IMenu {
    public Button GetInitialButton();
    public void Toggle(bool toggle);
    public void Open();
    public void Close();
}
