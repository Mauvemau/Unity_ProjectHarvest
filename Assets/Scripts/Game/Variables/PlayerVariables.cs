using UnityEngine;

[System.Serializable]
public class PlayerVariables {
    [field:SerializeField] public float MaxHealth { get; set; }
    [Tooltip("Multiplies the damage of all weapons")]
    [field:SerializeField] public float DamageMultiplier { get; set; }
    [Tooltip("Multiplies the attack speed of all weapons")]
    [field:SerializeField] public float AttackRateMultiplier { get; set; }
    [Tooltip("Multiplies the size of all weapon attacks")]
    [field:SerializeField] public float AttackSizeMultiplier { get; set; }
    [field:SerializeField] public float MovementSpeed { get; set; }
    [Tooltip("Expands the collectible collector radius for the player")]
    [field:SerializeField] public float CollectRange { get; set; }

    public static PlayerVariables operator +(PlayerVariables a, PlayerVariables b) {
        return new PlayerVariables {
            MaxHealth = a.MaxHealth + b.MaxHealth,
            DamageMultiplier = a.DamageMultiplier + b.DamageMultiplier,
            AttackRateMultiplier = a.AttackRateMultiplier + b.AttackRateMultiplier,
            AttackSizeMultiplier = a.AttackSizeMultiplier + b.AttackSizeMultiplier,
            MovementSpeed = a.MovementSpeed + b.MovementSpeed,
            CollectRange = a.CollectRange + b.CollectRange,
        };
    }
    public static PlayerVariables operator *(PlayerVariables a, PlayerVariables b) {
        return new PlayerVariables {
            MaxHealth = a.MaxHealth * b.MaxHealth,
            DamageMultiplier = a.DamageMultiplier + b.DamageMultiplier, // Multipliers add each other
            AttackRateMultiplier = a.AttackRateMultiplier + b.AttackRateMultiplier,
            AttackSizeMultiplier = a.AttackSizeMultiplier + b.AttackSizeMultiplier,
            MovementSpeed = a.MovementSpeed * b.MovementSpeed,
            CollectRange = a.CollectRange * b.CollectRange,
        };
    }
}
