using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private WeaponDisplaySlot[] slots;

    private void UpdateSlots(List<WeaponDisplayContainer> weaponDisplayContainers) {
        for (int i = 0; i < weaponDisplayContainers.Count; i++) {
            if (!slots[i]) return;
            slots[i].ClearDisplay();
            
            slots[i].SetDisplay(weaponDisplayContainers[i]);
        }
    }

    private void ClearAllSlots() {
        foreach (WeaponDisplaySlot slot in slots) {
            slot.ClearDisplay();
        }
    }
    
    private void Awake() {
        ClearAllSlots();
        WeaponUpgradeManager.OnUpdateInventory += UpdateSlots;
        MyGameManager.OnGameEnd += ClearAllSlots;
    }

    private void OnDestroy() {
        WeaponUpgradeManager.OnUpdateInventory -= UpdateSlots;
        MyGameManager.OnGameEnd -= ClearAllSlots;
    }
}
