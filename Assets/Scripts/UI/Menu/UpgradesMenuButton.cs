using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UpgradesMenuButton : MonoBehaviour {
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameAndLevelText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject selectorIndicator;
    public static event Action OnUpgradeMenuOptionSelected = delegate {};

    private Toggle _toggle;
    private Color _normalColor = Color.red;
    
    public void SetConfirmedSelection(bool selected) {
        if (_toggle) {
            ColorBlock cb = _toggle.colors;
            cb.normalColor = selected ? _toggle.colors.selectedColor : _normalColor;
            _toggle.colors = cb;
        }
        if (!selectorIndicator) return;
        selectorIndicator.SetActive(selected);
    }
    
    public void HandleSelection() {
        OnUpgradeMenuOptionSelected?.Invoke();
    }
    
    public void SetDisplay(WeaponDisplayContainer displayData) {
        itemIcon.sprite = displayData.icon;
        itemNameAndLevelText.text = displayData.weaponName + (displayData.level > 0 ? " (Level " + (displayData.level + 1) + ")" : "");
        itemDescriptionText.text = displayData.description;
    }
    
    public void SetVisible(bool visible) {
        gameObject.SetActive(visible);
    }

    private void Awake() {
        if (!TryGetComponent(out _toggle)) return;
        _normalColor = _toggle.colors.normalColor;
    }

    private void OnEnable() {
        SetConfirmedSelection(false);
    }
}

