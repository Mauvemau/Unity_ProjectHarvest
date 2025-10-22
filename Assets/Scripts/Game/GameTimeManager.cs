using System.Collections.Generic;
using UnityEngine;

public class Timer : ITimer {
    private float _currentTime = 0f;
    private bool _isRunning = false;
    private float _timeScale = 1f;

    public float CurrentTime => _currentTime;
    public bool IsRunning => _isRunning;

    public void SetTimeScale(float scale) {
        _timeScale = Mathf.Max(0f, scale);
    }

    public void Stop() {
        _isRunning = false;
    }

    public void Resume() {
        _isRunning = true;
    }

    public void Reset() {
        _currentTime = 0f;
    }

    public void Start() {
        _currentTime = 0f;
        _isRunning = true;
    }

    public void Update(float deltaTime) {
        if (!_isRunning) return;
        _currentTime += deltaTime * _timeScale;
    }
}

[System.Serializable]
public class GameTimeManager {
    [SerializeField] private BoolEventChannelSO onSetGamePaused;

    private readonly List<Timer> _activeTimers = new();

    public ITimer CreateTimer() {
        Timer timer = new Timer();
        _activeTimers.Add(timer);
        return timer;
    }
    
    public bool IsGamePaused() {
        return Time.timeScale <= 0f;
    }
    
    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
    
    public void SetGamePaused(bool paused) {
        SetTimeScale(paused ? 0f : 1f);
    }

    public void Update() {
        if (IsGamePaused()) return;
        
        foreach (Timer timer in _activeTimers) {
            timer.Update(Time.unscaledDeltaTime);
        }
    }

    public void OnEnable() {
        if (onSetGamePaused) {
            onSetGamePaused.OnEventRaised += SetGamePaused;
        }
    }

    public void OnDisable() {
        if (onSetGamePaused) {
            onSetGamePaused.OnEventRaised -= SetGamePaused;
        }
    }
}
