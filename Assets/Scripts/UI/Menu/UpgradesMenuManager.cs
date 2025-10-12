using System.Collections.Generic;
using UnityEngine;

public class UpgradesMenuManager : MonoBehaviour {
    [SerializeField] private int buttonsAmount = 4;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField, ReadOnly] private List<GameObject> buttons;
    
    private void Awake() {
        if (!buttonPrefab) return;
        for (int i = 0; i < buttonsAmount; i++) {
            GameObject newButton = Instantiate(buttonPrefab, transform);
            newButton.name = $"UpgradeButton_{i}";
            buttons.Add(newButton);
        }
    }
}
