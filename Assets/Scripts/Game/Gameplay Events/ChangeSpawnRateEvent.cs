using System;
using UnityEngine;

[System.Serializable]
public class ChangeSpawnRateEvent : GameplayEvent {
    [Header("Spawn Rate Settings")]
    [SerializeField] private float newSpawnRate = 1f;
    
    public static event Action<float> OnChangeContinuousSpawnRate = delegate {};

    private void SetContinuousSpawnRate() {
        OnChangeContinuousSpawnRate?.Invoke(newSpawnRate);
    }

    protected override void OnEventTriggered() {
        SetContinuousSpawnRate();
    }
}
