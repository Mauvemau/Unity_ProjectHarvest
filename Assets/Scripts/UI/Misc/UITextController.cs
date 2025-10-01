using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TMP_Text))]
public class UITextController : MonoBehaviour {
    [Header("Event Invoker")] 
    [SerializeField] private StringEventChannelSo onUpdateText;
    
    private TMP_Text _text;

    private void UpdateText(string newText) {
        if (!_text) return;
        _text.text = newText;
    }
    
    private void Awake() {
        if (!TryGetComponent(out _text)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_text)}\"");
        }
    }

    private void OnEnable() {
        if (onUpdateText) {
            onUpdateText.onEventRaised += UpdateText;
        }
    }

    private void OnDisable() {
        if (onUpdateText) {
            onUpdateText.onEventRaised -= UpdateText;
        }
    }
}
