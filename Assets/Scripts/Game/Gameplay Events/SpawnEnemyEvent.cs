using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnEnemyEvent : GameplayEvent {
    [Header("Enemies to Spawn")]
    [SerializeField] private List<GameObject> enemyBatch;
    
    public static event Action<List<GameObject>> OnSpawnEnemyBatch = delegate {};

    private void SpawnEnemyBatch() {
        OnSpawnEnemyBatch?.Invoke(enemyBatch);
    }

    protected override void OnEventTriggered() {
        SpawnEnemyBatch();
    }
}
