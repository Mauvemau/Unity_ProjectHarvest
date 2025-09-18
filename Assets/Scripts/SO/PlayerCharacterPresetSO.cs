using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for player stats
/// </summary>
[CreateAssetMenu(menuName = "Containers/Player Stats")]
public class PlayerCharacterPresetSO : ScriptableObject {
    [Header("Base Stats")]
    [field: SerializeField] public PlayerStats CharacterBaseStats { get; private set; }
    [field: SerializeField] public WeaponStats CharacterWeaponBonus { get; private set; }
    [Header("Visual")]
    [SerializeField] private Sprite sprite;
    [Header("Weapon")]
    [SerializeField] private List<GameObject> startingWeapons;
}
