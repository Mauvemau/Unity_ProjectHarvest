using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Containers/Weapon Upgrade Plan")]
public class WeaponUpgradePlanSO : ScriptableObject {
    [SerializeField] private List<GameObject> weaponLevel;
    
    public GameObject GetWeaponOfLevel(int level) {
        return !weaponLevel[level] ? null : weaponLevel[level];
    }
}
