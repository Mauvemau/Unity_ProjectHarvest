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

    private GameObject _lastSelected;
    private int _currentlySelectedOption = 0;

    private void DisableAllButtons() {
        foreach (UpgradesMenuButton button in optionButtons) {
            button.SetVisible(false);
        }
    }
    
    private void HandleUpgradesReceived(List<WeaponDisplayContainer> upgrades) {
        if (upgrades == null || upgrades.Count == 0) {
            return;
        }
        DisableAllButtons();
        
        int count = Mathf.Min(upgrades.Count, buttonsAmount);
        
        for (int i = 0; i < count; i++) {
            UpgradesMenuButton upgradeButton = optionButtons[i];
            upgradeButton.SetVisible(true);
            
            if (upgradeButton) {
                upgradeButton.SetDisplay(upgrades[i]);
            } 
        }
        
        _currentlySelectedOption = 0;
        if (!eventSystem) return;
        eventSystem.SetSelectedGameObject(optionButtons[_currentlySelectedOption].gameObject);
    }
    
    private void OnSubmit() {
        if (!eventSystem) return;
        if (currentlySelectedButton) {
            foreach (UpgradesMenuButton button in optionButtons) {
                button.SetSelected(button.gameObject == currentlySelectedButton);
            }
        }
        Debug.Log($"{name}: Selected option {_currentlySelectedOption}!");
        eventSystem.SetSelectedGameObject(confirmButton.gameObject);
    }

    private void OnCancel() {
        if (!eventSystem || optionButtons.Count <= 0) return;
        foreach (UpgradesMenuButton button in optionButtons) {
            button.SetSelected(false);
        }
        
        eventSystem.SetSelectedGameObject(optionButtons[_currentlySelectedOption].gameObject);
    }
    
    private void Update() {
        if (!eventSystem) return;

        GameObject current = eventSystem.currentSelectedGameObject;
        
        if (current != _lastSelected && current) {
            _lastSelected = current;
            currentlySelectedButton = current;
            
            for (int i = 0; i < optionButtons.Count; i++) {
                if (optionButtons[i].gameObject != current) continue;
                _currentlySelectedOption = i;
                break;
            }
        }
        
        if (current) return;

        if (_lastSelected) {
            eventSystem.SetSelectedGameObject(_lastSelected);
        }
        else if (optionButtons.Count > 0) {
            eventSystem.SetSelectedGameObject(optionButtons[_currentlySelectedOption].gameObject);
        }
    }
    
    private void Awake() {
        if (!buttonPrefab) return;
        
        optionButtons = new List<UpgradesMenuButton>();
        
        for (int i = 0; i < buttonsAmount; i++) {
            GameObject newButton = Instantiate(buttonPrefab, transform);
            newButton.name = $"UpgradeButton_{i}";
            UpgradesMenuButton upgradeButton = newButton.GetComponent<UpgradesMenuButton>();
            optionButtons.Add(upgradeButton);
            upgradeButton.SetVisible(false);
        }

        WeaponUpgradeManager.OnUpgradesReady += HandleUpgradesReceived;
    }
    
    private void OnDestroy() {
        WeaponUpgradeManager.OnUpgradesReady -= HandleUpgradesReceived;
    }

    private void OnEnable() {
        InputManager.OnUISubmitInputCancelled += OnSubmit;
        InputManager.OnUICancelInputStarted += OnCancel;
    }

    private void OnDisable() {
        InputManager.OnUISubmitInputCancelled -= OnSubmit;
        InputManager.OnUICancelInputStarted -= OnCancel;
    }
}
