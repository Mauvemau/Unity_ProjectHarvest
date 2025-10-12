using System.Linq;
using UnityEngine;

public class WeaponContainer {
    
}

[System.Serializable]
public class WeaponUpgradeManager {
    [Header("Database")]
    [SerializeField] private WeaponUpgradePlanSO[] weaponDatabase;

    [Header("References")] 
    [SerializeField] private WeaponInventoryManager playerInventoryReference;

    [Header("Settings")] 
    [SerializeField] private WeaponUpgradePlanSO playerStartingPlan;

    [Header("Debug")]
    [SerializeField, ReadOnly] private WeaponUpgradePlanSO[] playerCurrentlyEquippedPlans;
    
    public void DebugUpgradeStaringWeapon() {
        UpgradePlayerWeapon(playerStartingPlan);
    }
    
    private void UpgradePlayerWeapon(WeaponUpgradePlanSO plan) {
        if (!playerCurrentlyEquippedPlans.Contains(plan)) {
            Debug.LogError($"{nameof(WeaponUpgradeManager)}: trying to upgrade plan not currently equipped by the player!");
            return;
        }
        playerInventoryReference.UpgradeWeapon(plan);
    }
    
    private void EquipPlanOnPlayer(WeaponUpgradePlanSO planToEquip) {
        if (!playerInventoryReference) return;
        if (!planToEquip) {
            Debug.LogError($"{nameof(WeaponUpgradeManager)}: trying to equip invalid plan!");
            return;
        }
        playerInventoryReference.EquipPlan(playerStartingPlan);
    }
    
    private void FetchCurrentlyEquippedPlans() {
        playerCurrentlyEquippedPlans = playerInventoryReference.GetEquippedPlans();
    }

    public void Init() {
        if (!playerInventoryReference) return;
        EquipPlanOnPlayer(playerStartingPlan);
        
        FetchCurrentlyEquippedPlans();
    }
    
    public void OnEnable() {
        if (!playerInventoryReference) {
            Debug.LogError($"{nameof(WeaponUpgradeManager)} is missing a player reference!");
        }
        
    }

    public void OnDisable() {
        
    }
}
