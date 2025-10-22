using UnityEngine.UI;

public interface IMenu {
    public Button InitialButton();
    public void Toggle(bool toggle);
    public void Open();
    public void Close();
}
