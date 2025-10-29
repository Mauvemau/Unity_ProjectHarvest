using UnityEngine;

public class MusicManager : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private AK.Wwise.Event startAudioEvent;

    [Header("States")] 
    [SerializeField] private AK.Wwise.State inMainMenuState;
    [SerializeField] private AK.Wwise.State inCombatState;
    [SerializeField] private AK.Wwise.State inPausedGameState;

    [Header("Settings")] 
    [SerializeField] private bool autoplayOnStart = false;
    
    [Header("Event Listeners")]
    [SerializeField] private BoolEventChannelSO onSetGamePaused;
    
    private System.Action _onGameStartHandler;
    private System.Action _onGameEndHandler;
    private void ChangeState(AK.Wwise.State newState) {
        Debug.Log($"Changing music state to {newState}!");
        newState?.SetValue();
    }
    
    private void HandleGamePaused(bool isPaused) {
        ChangeState(isPaused ? inPausedGameState : inCombatState);
    }
    
    private void Start() {
        if (!autoplayOnStart) return;
        startAudioEvent.Post(gameObject);
    }

    private void OnEnable() {
        _onGameStartHandler = () => ChangeState(inCombatState);
        _onGameEndHandler   = () => ChangeState(inMainMenuState);

        MyGameManager.OnGameStart += _onGameStartHandler;
        MyGameManager.OnGameEnd   += _onGameEndHandler;

        if (onSetGamePaused) {
            onSetGamePaused.OnEventRaised += HandleGamePaused;
        }
    }

    private void OnDisable() {
        MyGameManager.OnGameStart -= _onGameStartHandler;
        MyGameManager.OnGameEnd   -= _onGameEndHandler;
        
        if (onSetGamePaused) {
            onSetGamePaused.OnEventRaised -= HandleGamePaused;
        }
    }
}
