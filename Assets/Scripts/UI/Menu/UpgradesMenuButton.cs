using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UpgradesMenuButton : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameAndLevelText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private Image levelProgressBarFill;
    [SerializeField] private GameObject levelProgressBar;
    [SerializeField] private GameObject selectorIndicator;

    [Header("Settings")] 
    [SerializeField] private int maxLevel = 6;
    
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


        if (levelProgressBar) {
            levelProgressBar.SetActive(displayData.level > 0);
        }
        
        levelProgressBarFill.fillAmount = (displayData.level / (float)maxLevel);
    }
    
    public void SetVisible(bool visible) {
        gameObject.SetActive(visible);
    }

    private void Awake() {
        if (levelProgressBarFill) {
            levelProgressBarFill.type = Image.Type.Filled;
            levelProgressBarFill.fillMethod = Image.FillMethod.Horizontal;
            levelProgressBarFill.fillAmount = 0;
        }

        if (levelProgressBar) {
            levelProgressBar.SetActive(false);
        }
        
        if (!TryGetComponent(out _toggle)) return;
        _normalColor = _toggle.colors.normalColor;
    }

    private void OnEnable() {
        SetConfirmedSelection(false);
    }
}

