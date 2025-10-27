using System;
using UnityEngine;

[System.Serializable]
public class GameplayEvent {
    [SerializeField] private float timestampInSeconds = 0f;
    [SerializeField] private CustomCallback eventCallbacks;

    [Header("Debug")] 
    [SerializeField, ReadOnly] private string timestampFormatted;

    private bool _triggered = false;
    
    public float TimestampInSeconds => timestampInSeconds;
    
    public void TriggerAllCallbacks() {
        if (_triggered) return;
        eventCallbacks.Invoke();
        _triggered = true;
    }
    
    public void OnValidate() {
        TimeSpan t = TimeSpan.FromSeconds(timestampInSeconds);
        timestampFormatted = $"{(int)t.Minutes:D2}:{(int)t.Seconds:D2}";
    }
}

[System.Serializable]
public class GameplayEventManager {
    [SerializeField] private GameplayEvent[] gameplayEvents;

    public void Update(float currentGameTime) {
        foreach (GameplayEvent gameplayEvent in gameplayEvents) {
            if(currentGameTime < gameplayEvent.TimestampInSeconds) return;
            gameplayEvent.TriggerAllCallbacks();
        }
    }

    public void OnValidate() {
        foreach (GameplayEvent gameplayEvent in gameplayEvents) {
            gameplayEvent.OnValidate();
        }
    }
}
