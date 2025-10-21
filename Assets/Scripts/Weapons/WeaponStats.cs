using UnityEngine;

[System.Serializable]
public class WeaponStats {
    public float attackDamage;
    [Min(0)] public float attackRateInSeconds;
    [Min(0)] public float attackSize;
}
