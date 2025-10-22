using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TimerDisplay : MonoBehaviour {
    private TMP_Text _text;
    
    private void UpdateTimer(float currentTime) {
        _text.text = $"{(int)(currentTime / 60):00}:{(int)(currentTime % 60):00}";
    }
    
    private void Awake() {
        if (!TryGetComponent(out _text)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_text)}\"");
        }
    }

    private void OnEnable() {
        MyGameManager.OnUpdateGameTimer += UpdateTimer;
    }
    
    private void OnDisable() {
        MyGameManager.OnUpdateGameTimer -= UpdateTimer;
    }
}
