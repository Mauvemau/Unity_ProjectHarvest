using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectibleDrop {
    [Header("Prefab")]
    [SerializeField] private GameObject collectiblePrefab;
    [Header("Chance")]
    [field: SerializeField, Range(0f, 1f)] public float DropChance { get; set; }

    public void RequestDrop(CentralizedFactory centralizedFactory, Vector2 position) {
        if (!centralizedFactory) return;
        centralizedFactory.Create(collectiblePrefab, position, Quaternion.identity, Vector3.one);
    }
}

[System.Serializable]
public class DropManager {
    [SerializeField] private List<CollectibleDrop> drops;
    
    [Header("Settings")]
    [SerializeField] private float multipleDropOffsetDistance = 1f;

    private CentralizedFactory _centralizedFactory;

    private bool TryFindCentralizedFactory() {
        return _centralizedFactory || ServiceLocator.TryGetService(out _centralizedFactory);
    }

    /// <summary>
    /// Rolls a random number and triggers a drop if it falls within a drop chance
    /// </summary>
    public void HandleRequestDrops(Vector2 position) {
        if (drops == null || drops.Count == 0) return;
        if (!TryFindCentralizedFactory()) return;

        float roll = Random.value;
        float cumulative = 0f;
        
        int droppedCount = 0;

        foreach (var drop in drops) {
            cumulative += drop.DropChance;
            if (!(roll <= cumulative)) continue;
            Vector2 finalPosition = position;
                
            if (droppedCount > 0) {
                Vector2 offset = Random.insideUnitCircle * multipleDropOffsetDistance;
                finalPosition += offset;
            }

            drop.RequestDrop(_centralizedFactory, finalPosition);
            droppedCount++;
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

            if (!(total > 1f)) continue;
            float excess = total - 1f;
            drops[i].DropChance -= excess;
            total = 1f;

            Debug.LogWarning($"[CollectibleDropManager] Total drop chance exceeded 100%. " +
                             $"Clamped element at index {i} to keep total at 100%.");
        }
    }
}
