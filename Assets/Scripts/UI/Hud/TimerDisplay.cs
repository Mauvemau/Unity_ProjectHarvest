using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TimerDisplay : MonoBehaviour {
    [Header("Time Limit Settings")] 
    [SerializeField] private float timeLimit = 0;
    [SerializeField] private bool countdown = false;
    
    [Header("Visual Settings")]
    [SerializeField] private Color timeLimitReachedColor =  Color.red; 
    
    private TMP_Text _text;
    private Color _defaultColor;
    
    private void UpdateTimer(float currentTime) {
        float displayTime;

        if (timeLimit > 0f) {
            if (countdown) {
                displayTime = Mathf.Max(timeLimit - currentTime, 0f);
                _text.color = displayTime <= 0f ? timeLimitReachedColor : _defaultColor;
            } else {
                displayTime = currentTime;
                _text.color = currentTime >= timeLimit ? timeLimitReachedColor : _defaultColor;
            }
        } else {
            displayTime = countdown ? 0f : currentTime;
            _text.color = _defaultColor;
        }

        int minutes = (int)(displayTime / 60);
        int seconds = (int)(displayTime % 60);
        _text.text = $"{minutes:00}:{seconds:00}";
    }
    
    private void Awake() {
        if (!TryGetComponent(out _text)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_text)}\"");
            return;
        }
        _defaultColor = _text.color;
    }

    private void OnEnable() {
        MyGameManager.OnUpdateGameTimer += UpdateTimer;
    }
    
    private void OnDisable() {
        MyGameManager.OnUpdateGameTimer -= UpdateTimer;
    }
}
