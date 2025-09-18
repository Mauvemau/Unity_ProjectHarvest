using UnityEngine;

/// <summary>
/// Container for player stats
/// </summary>
[CreateAssetMenu(menuName = "Containers/Player Stats")]
public class PlayerStatsContainerSO : ScriptableObject {
    [SerializeField] public PlayerStats playerStats;
}
