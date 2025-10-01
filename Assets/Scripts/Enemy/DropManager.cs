using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectibleDrop {
    [Header("Event Invoker")]
    [SerializeField] private Vector2EventChannelSo onRequestDropChannel;
    [Header("Chance")]
    [field: SerializeField, Range(0f, 1f)] public float DropChance { get; set; }

    public void RequestDrop(Vector2 position) {
        if (!onRequestDropChannel) return;
        onRequestDropChannel.RaiseEvent(position);
    }
}

[System.Serializable]
public class DropManager {
    [SerializeField] private List<CollectibleDrop> drops;

    /// <summary>
    /// Rolls a random number and triggers a drop if it falls within a drop chance
    /// </summary>
    public void HandleRequestDrops(Vector2 position) {
        if (drops == null || drops.Count == 0)
            return;

        float roll = Random.value;
        float cumulative = 0f;

        foreach (var drop in drops) {
            cumulative += drop.DropChance;
            if (!(roll <= cumulative)) continue;
            drop.RequestDrop(position);
            return;
        }
    }
    
    private void OnValidate() {
        if (drops == null || drops.Count == 0) return;

        float total = 0f;

        for (int i = 0; i < drops.Count; i++) {
            if (total >= 1f) {
                drops[i].DropChance = 0f;
                continue;
            }

            total += drops[i].DropChance;

            if (total > 1f) {
                float excess = total - 1f;
                drops[i].DropChance -= excess;
                total = 1f;

                Debug.LogWarning($"[CollectibleDropManager] Total drop chance exceeded 100%. " +
                                 $"Clamped element at index {i} to keep total at 100%.");
            }
        }
    }
}
