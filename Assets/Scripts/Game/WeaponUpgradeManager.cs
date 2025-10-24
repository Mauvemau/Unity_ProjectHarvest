using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private int upgradesAvailablePerLevel = 3;
    
    [Header("Event Listeners")] 
    [SerializeField] private VoidEventChannelSO onLevelUpChannel;
    
    [Header("Debug")]
    [SerializeField, ReadOnly] private WeaponUpgradePlanSO[] playerCurrentlyEquippedPlans;
    [SerializeField, ReadOnly] private List<WeaponUpgradePlanSO> currentlyAvailablePlans = new List<WeaponUpgradePlanSO>();

    private int _inventoryLimit = 0;
    
    public static event Action<List<WeaponDisplayContainer>> OnUpgradesReady = delegate {};
    public static event Action<List<WeaponDisplayContainer>> OnUpdateInventory = delegate {};

    public List<WeaponDisplayContainer> GetSelectableWeapons(int count) {
        List<WeaponDisplayContainer> result = new List<WeaponDisplayContainer>();
        HashSet<WeaponUpgradePlanSO> usedPlans = new HashSet<WeaponUpgradePlanSO>();

        bool inventoryFull = playerCurrentlyEquippedPlans.Length >= _inventoryLimit;
        
        List<WeaponUpgradePlanSO> shuffledPlans = new List<WeaponUpgradePlanSO>(weaponDatabase);
        for (int i = 0; i < shuffledPlans.Count; i++) {
            int swapIndex = Random.Range(i, shuffledPlans.Count);
            (shuffledPlans[i], shuffledPlans[swapIndex]) = (shuffledPlans[swapIndex], shuffledPlans[i]);
        }

        foreach (WeaponUpgradePlanSO plan in shuffledPlans) {
            if (usedPlans.Contains(plan)) continue;

            bool isEquipped = playerCurrentlyEquippedPlans.Contains(plan);

            if (inventoryFull && !isEquipped) {
                continue;
            }

            int nextLevel = 0;
            bool canUpgrade = false;

            if (isEquipped) {
                int currentLevel = playerInventoryReference.GetPlanLevel(plan);
                nextLevel = currentLevel + 1;
                canUpgrade = nextLevel < plan.UpgradesCount;
                
                if (inventoryFull && !canUpgrade)
                    continue;
            }
            
            if (!isEquipped) {
                result.Add(new WeaponDisplayContainer(
                    plan.WeaponName,
                    0,
                    plan.GetDescriptionOfLevel(0),
                    plan.WeaponIcon
                ));
            } 
            else if (canUpgrade) {
                result.Add(new WeaponDisplayContainer(
                    plan.WeaponName,
                    nextLevel,
                    plan.GetDescriptionOfLevel(nextLevel),
                    plan.WeaponIcon
                ));
            }

            usedPlans.Add(plan);

            if (result.Count >= count) {
                break;
            }
        }

        currentlyAvailablePlans = usedPlans.ToList();
        return result;
    }


    private void HandleLevelUpgrades() {
        List<WeaponDisplayContainer> levelUpWeapons = GetSelectableWeapons(upgradesAvailablePerLevel);
        OnUpgradesReady?.Invoke(levelUpWeapons);
    }

    private void HandleConfirmUpgrade(int option) {
        if (currentlyAvailablePlans.Count <= 0) return;
        if (option < 0 || option >= currentlyAvailablePlans.Count) return;
        WeaponUpgradePlanSO selection = currentlyAvailablePlans[option];
        currentlyAvailablePlans.Clear();
        if (!selection) return;
        if (playerCurrentlyEquippedPlans.Contains(selection)) {
            Debug.Log($"{nameof(WeaponUpgradeManager)}: Upgrading {selection.WeaponName}");
            UpgradePlayerWeapon(selection);
            return;
        }
        Debug.Log($"{nameof(WeaponUpgradeManager)}: Equipping {selection.WeaponName} plan");
        EquipPlanOnPlayer(selection);
    }
    
    private void UpgradePlayerWeapon(WeaponUpgradePlanSO plan) {
        if (!playerCurrentlyEquippedPlans.Contains(plan)) {
            Debug.LogError($"{nameof(WeaponUpgradeManager)}: trying to upgrade plan not currently equipped by the player!");
            return;
        }
        playerInventoryReference.UpgradeWeapon(plan);
        FetchCurrentlyEquippedPlans();
    }
    
    private void EquipPlanOnPlayer(WeaponUpgradePlanSO planToEquip) {
        if (!playerInventoryReference) return;
        if (!planToEquip) {
            Debug.LogError($"{nameof(WeaponUpgradeManager)}: trying to equip invalid plan!");
            return;
        }
        playerInventoryReference.EquipPlan(planToEquip);
        FetchCurrentlyEquippedPlans();
    }

    /// <summary>
    /// Updates the frontend part of the inventory
    /// </summary>
    private void UpdateInventoryDisplay() {
        if (playerCurrentlyEquippedPlans.Length <= 0) return;
        List<WeaponDisplayContainer> inventoryDisplay = new List<WeaponDisplayContainer>();
        
        foreach (WeaponUpgradePlanSO plan in playerCurrentlyEquippedPlans) {
            int nextLevel = playerInventoryReference.GetPlanLevel(plan);
            inventoryDisplay.Add(new WeaponDisplayContainer(
                plan.WeaponName,
                nextLevel,
                plan.GetDescriptionOfLevel(nextLevel),
                plan.WeaponIcon
            ));
        }
        
        OnUpdateInventory?.Invoke(inventoryDisplay);
    }
    
    private void FetchCurrentlyEquippedPlans() {
        playerCurrentlyEquippedPlans = playerInventoryReference.GetEquippedPlans();
        UpdateInventoryDisplay();
    }

    public void UnequipAll() {
        playerInventoryReference.ClearInventory();
        FetchCurrentlyEquippedPlans();
    }
    
    public void Init() {
        if (!playerInventoryReference) return;
        EquipPlanOnPlayer(playerStartingPlan);
        _inventoryLimit = playerInventoryReference.WeaponInventoryLimit;
    }
    
    public void OnEnable() {
        if (!playerInventoryReference) {
            Debug.LogError($"{nameof(WeaponUpgradeManager)} is missing a player reference!");
        }

        if (onLevelUpChannel) {
            onLevelUpChannel.OnEventRaised += HandleLevelUpgrades;
        }

        UpgradesMenuManager.OnUpgradeOptionConfirmed += HandleConfirmUpgrade;
    }

    public void OnDisable() {
        if (onLevelUpChannel) {
            onLevelUpChannel.OnEventRaised -= HandleLevelUpgrades;
        }
        
        UpgradesMenuManager.OnUpgradeOptionConfirmed -= HandleConfirmUpgrade;
    }
}
