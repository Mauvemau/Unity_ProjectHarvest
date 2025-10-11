using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquippedWeaponTracker {
    public int currentLevel = 0;
    public GameObject weaponReference;
}

/// <summary>
/// Contains and manages weapons and upgrades
/// </summary>
public class WeaponManager: MonoBehaviour {
    [Header("Inventory Settings")]
    [SerializeField, Min(0)] private int weaponInventoryLimit = 4;

    [Header("Debug")]
    [SerializeField, ReadOnly] private List<WeaponUpgradePlanSO> equippedPlans;
    [SerializeField, ReadOnly] private List<EquippedWeaponTracker> equippedWeapons;

    private Factory _weaponFactory;

    /// <summary>
    /// Enacts a weapon upgrade plan, if there's inventory space for it, and spawns the first weapon (level 0) from that plan
    /// </summary>
    public void EquipPlan(WeaponUpgradePlanSO upgradePlan) {
        if (IsPlanEquipped(upgradePlan)) {
            Debug.LogWarning($"{name}: Plan already equipped.");
            return;
        }
        if (equippedPlans.Count >= weaponInventoryLimit) {
            Debug.LogError($"{name}: Cannot equip upgrade plan; Inventory Limit Exceeded");
            return;
        }
        
        GameObject prefab = upgradePlan.GetWeaponOfLevel(0);
        if (!prefab) {
            Debug.LogError($"{name}: Cannot equip upgrade plan; There are no prefabs in this plan");
            return;
        }
        
        equippedPlans.Add(upgradePlan);
        
        _weaponFactory.SetPrefabToCreate(prefab);
        GameObject weapon = _weaponFactory.Create(transform.position, Quaternion.identity, Vector3.one, transform);
        EquippedWeaponTracker weaponTracker = new EquippedWeaponTracker {
            weaponReference = weapon,
            currentLevel = 0
        };
        equippedWeapons.Add(weaponTracker);
    }

    /// <summary>
    /// If the weapon plan has more levels, it destroys the currently equipped weapon and spawns one 1 level higher
    /// </summary>
    public void UpgradeWeapon(WeaponUpgradePlanSO upgradePlan) {
        int index = GetEquippedPlanIndex(upgradePlan);
        if (index == -1) {
            Debug.LogError($"{name}: Cannot upgrade; Plan not equipped.");
            return;
        }

        EquippedWeaponTracker tracker = equippedWeapons[index];
        int nextLevel = tracker.currentLevel + 1;

        GameObject nextWeapon = upgradePlan.GetWeaponOfLevel(nextLevel);
        if (!nextWeapon) {
            Debug.LogWarning($"{name}: No weapon upgrade available for level {nextLevel}.");
            return;
        }
        _weaponFactory.SetPrefabToCreate(nextWeapon);
        
        _weaponFactory.Destroy(tracker.weaponReference);

        tracker.weaponReference = _weaponFactory.Create(transform.position, Quaternion.identity, Vector3.one, transform);
        tracker.currentLevel = nextLevel;
    }

    public void ClearInventory() {
        foreach (EquippedWeaponTracker tracker in equippedWeapons) {
            if (tracker.weaponReference != null) {
                _weaponFactory.Destroy(tracker.weaponReference);
            }
        }
        equippedWeapons.Clear();
        equippedPlans.Clear();
    }
    
    private int GetEquippedPlanIndex(WeaponUpgradePlanSO plan) {
        return equippedPlans.IndexOf(plan);
    }

    private bool IsPlanEquipped(WeaponUpgradePlanSO plan) {
        return GetEquippedPlanIndex(plan) != -1;
    }
}
