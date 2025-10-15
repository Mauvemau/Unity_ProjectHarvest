using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradesMenuManager : MonoBehaviour {
    [Header("Menu Settings")]
    [SerializeField] private int buttonsAmount = 4;
    [SerializeField] private GameObject buttonPrefab;

    [Header("References")] 
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Button confirmButton;

    [Header("Debug")]
    [SerializeField, ReadOnly] private List<UpgradesMenuButton> optionButtons;
    [SerializeField, ReadOnly] private GameObject currentlySelectedButton;

    public static event Action<int> OnUpgradeOptionConfirmed = delegate {};
    
    private GameObject _lastSelected;
    private int _currentlySelectedOption = -1;

    public void ConfirmSelection() {
        if (_currentlySelectedOption >= 0) {
            OnUpgradeOptionConfirmed?.Invoke(_currentlySelectedOption);
        }
    }
    
    private void DisableAllButtons() {
        foreach (UpgradesMenuButton button in optionButtons) {
            button.SetVisible(false);
        }
    }
    
    private void HandleUpgradesReceived(List<WeaponDisplayContainer> upgrades) {
        if (upgrades == null || upgrades.Count == 0) {
            return;
        }
        Debug.Log($"{name}: Upgrades received! {upgrades.Count} in total!");
        DisableAllButtons();
        
        int count = Mathf.Min(upgrades.Count, buttonsAmount);
        
        for (int i = 0; i < count; i++) {
            UpgradesMenuButton upgradeButton = optionButtons[i];
            upgradeButton.SetVisible(true);
            
            if (upgradeButton) {
                upgradeButton.SetDisplay(upgrades[i]);
            } 
        }
        
        if (!eventSystem) return;
        eventSystem.SetSelectedGameObject(optionButtons[0].gameObject);
    }
    
    private void OnSubmit() {
        if (!eventSystem) return;
        
        for (int i = 0; i < optionButtons.Count; i++) {
            if (optionButtons[i].gameObject != currentlySelectedButton) continue;
            _currentlySelectedOption = i;
            break;
        }
        
        if (currentlySelectedButton) {
            foreach (UpgradesMenuButton button in optionButtons) {
                button.SetConfirmedSelection(button.gameObject == currentlySelectedButton);
            }
        }
        Debug.Log($"{name}: Selected option {_currentlySelectedOption}!");
        
        if (!confirmButton || _currentlySelectedOption < 0) return;
        eventSystem.SetSelectedGameObject(confirmButton.gameObject);
        confirmButton.interactable = true;
    }

    private void OnCancel() {
        if (!eventSystem || optionButtons.Count <= 0) return;
        foreach (UpgradesMenuButton button in optionButtons) {
            button.SetConfirmedSelection(false);
        }
        
        if (confirmButton) {
            confirmButton.interactable = false;
        }
        
        eventSystem.SetSelectedGameObject(optionButtons[_currentlySelectedOption].gameObject);
        _currentlySelectedOption = -1;
    }
    
    private void Update() {
        if (!eventSystem) return;

        GameObject current = eventSystem.currentSelectedGameObject;
        
        if (current != _lastSelected && current) {
            _lastSelected = current;
            currentlySelectedButton = current;
        }
        
        if (current) return;

        if (_lastSelected) {
            eventSystem.SetSelectedGameObject(_lastSelected);
        }
        else if (optionButtons.Count > 0) {
            eventSystem.SetSelectedGameObject(optionButtons[0].gameObject);
        }
    }
    
    private void Awake() {
        WeaponUpgradeManager.OnUpgradesReady += HandleUpgradesReceived;
        if (!buttonPrefab) return;
        
        optionButtons = new List<UpgradesMenuButton>();
        
        for (int i = 0; i < buttonsAmount; i++) {
            GameObject newButton = Instantiate(buttonPrefab, transform);
            newButton.name = $"UpgradeButton_{i}";
            UpgradesMenuButton upgradeButton = newButton.GetComponent<UpgradesMenuButton>();
            optionButtons.Add(upgradeButton);
            upgradeButton.SetVisible(false);
        }
    }
    
    private void OnDestroy() {
        WeaponUpgradeManager.OnUpgradesReady -= HandleUpgradesReceived;
    }

    private void OnEnable() {
        if (confirmButton) {
            confirmButton.interactable = false;
        }
        
        InputManager.OnUICancelInputStarted += OnCancel;
        UpgradesMenuButton.OnUpgradeMenuOptionSelected += OnSubmit;
    }

    private void OnDisable() {
        if (confirmButton) {
            confirmButton.interactable = false;
        }
        
        InputManager.OnUICancelInputStarted -= OnCancel;
        UpgradesMenuButton.OnUpgradeMenuOptionSelected -= OnSubmit;
    }
}
