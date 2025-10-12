using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeContainer {
    public GameObject weaponPrefab;
    [TextArea] public string upgradeDescription;
}

[CreateAssetMenu(menuName = "Containers/Weapon Upgrade Plan")]
public class WeaponUpgradePlanSO : ScriptableObject {
    [SerializeField] private List<UpgradeContainer> weaponLevel;
    
    public GameObject GetWeaponOfLevel(int level) {
        return !weaponLevel[level].weaponPrefab ? null : weaponLevel[level].weaponPrefab;
    }

    public string GetDescriptionOfLevel(int level) {
        return weaponLevel[level].upgradeDescription;
    }

    public UpgradeContainer GetContainer(int level) {
        return weaponLevel[level] == null ? null : weaponLevel[level];
    }

    public int GetUpgradesCount => weaponLevel.Count;
}
