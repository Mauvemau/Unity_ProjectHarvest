using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WeaponDisplayContainer {
    public string weaponName;
    public int level;
    public string description;
    public Sprite icon;

    public WeaponDisplayContainer(string name, int level, string description, Sprite icon) {
        weaponName = name;
        this.level = level;
        this.description = description;
        this.icon = icon;
    }
}

[System.Serializable]
public class WeaponUpgradeManager {
    [Header("Database")]
    [SerializeField] private WeaponUpgradePlanSO[] weaponDatabase;

    [Header("References")] 
    [SerializeField] private WeaponInventoryManager playerInventoryReference;

    [Header("Settings")] 
    [SerializeField] private WeaponUpgradePlanSO playerStartingPlan;

    [Header("Event Listeners")] 
    [SerializeField] private VoidEventChannelSO onLevelUpChannel;
    
    [Header("Debug")]
    [SerializeField, ReadOnly] private WeaponUpgradePlanSO[] playerCurrentlyEquippedPlans;
    
    public void DebugUpgradeStaringWeapon() {
        UpgradePlayerWeapon(playerStartingPlan);
    }
    
    public List<WeaponDisplayContainer> GetSelectableWeapons(int count) {
        List<WeaponDisplayContainer> result = new List<WeaponDisplayContainer>();
        HashSet<WeaponUpgradePlanSO> usedPlans = new HashSet<WeaponUpgradePlanSO>();
        
        List<WeaponUpgradePlanSO> shuffledPlans = new List<WeaponUpgradePlanSO>(weaponDatabase);
        for (int i = 0; i < shuffledPlans.Count; i++) {
            int swapIndex = Random.Range(i, shuffledPlans.Count);
            (shuffledPlans[i], shuffledPlans[swapIndex]) = (shuffledPlans[swapIndex], shuffledPlans[i]);
        }

        foreach (WeaponUpgradePlanSO plan in shuffledPlans) {
            if (usedPlans.Contains(plan)) continue;

            bool isEquipped = playerCurrentlyEquippedPlans.Contains(plan);
            
            if (!isEquipped) {
                result.Add(new WeaponDisplayContainer(
                    plan.WeaponName,
                    0,
                    plan.GetDescriptionOfLevel(0),
                    plan.WeaponIcon
                ));
                usedPlans.Add(plan);
            } else {
                int currentLevel = playerInventoryReference.GetPlanLevel(plan);
                int nextLevel = currentLevel + 1;

                if (nextLevel < plan.UpgradesCount) {
                    result.Add(new WeaponDisplayContainer(
                        plan.WeaponName,
                        nextLevel,
                        plan.GetDescriptionOfLevel(nextLevel),
                        plan.WeaponIcon
                    ));
                    usedPlans.Add(plan);
                }
            }

            if (result.Count >= count) break;
        }

        return result;
    }

    private void HandleLevelUpgrades() {
        List<WeaponDisplayContainer> levelUpWeapons = GetSelectableWeapons(3);
        for (int i = 1; i <= levelUpWeapons.Count; i++) {
            WeaponDisplayContainer current =  levelUpWeapons[i - 1];
            Debug.Log($"[{i}] {current.weaponName} (Level {current.level})\n{current.description}.\n");
        }
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

        if (onLevelUpChannel) {
            onLevelUpChannel.OnEventRaised += HandleLevelUpgrades;
        }
    }

    public void OnDisable() {
        if (onLevelUpChannel) {
            onLevelUpChannel.OnEventRaised -= HandleLevelUpgrades;
        }
    }
}
