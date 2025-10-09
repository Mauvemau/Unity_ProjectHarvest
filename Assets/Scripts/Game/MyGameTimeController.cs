using UnityEngine;

[System.Serializable]
public class MyGameTimeController {
    [SerializeField] private BoolEventChannelSO onSetGamePaused;

    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
    
    public void SetGamePaused(bool paused) {
        SetTimeScale(paused ? 0f : 1f);
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
