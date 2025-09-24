using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextVersionDisplay : MonoBehaviour {
    [SerializeField] private string versionPrefix = "v";
    [SerializeField] private string specialVersionText = "";

    private TMP_Text _text;

    private void Awake() {
        if (!TryGetComponent(out _text)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_text)}\"");
            return;
        }

        _text.text = $"{specialVersionText} {versionPrefix}{Application.version}";
    }
}
