using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradesMenuButton : MonoBehaviour {
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameAndLevelText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject selectorIndicator;

    public static event Action OnUpgradeMenuOptionSelected = delegate {};
    
    public void SetConfirmedSelection(bool selected) {
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
    
    private void OnEnable() {
        SetConfirmedSelection(false);
    }
}

