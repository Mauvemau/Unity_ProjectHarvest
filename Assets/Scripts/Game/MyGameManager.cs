using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    [Header("Weapon Upgrades Manager")] 
    [SerializeField] private WeaponUpgradeManager weaponUpgradeManager;
    
    [Header("Global Variables")]
    [SerializeField] private GlobalVariableManager globalVariableManager;

    [Header("References")]
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private SpawnerPH spawnManager;
    [SerializeField] private InputManager inputManager;

    [Header("Game Time")] 
    [SerializeField] private GameTimeManager timeManager;
    [SerializeField] private float timerPollingInterval = 1f;

    [Header("Event Invokers")] 
    [SerializeField] private VoidEventChannelSO onDefeatChannel;
    [SerializeField] private BoolEventChannelSO onToggleHudChannel;

    [Header("Event Listeners")]
    [SerializeField] private VoidEventChannelSO onStartGameChannel;
    [SerializeField] private VoidEventChannelSO onEndGameChannel;
    
    [Header("Debug Controls")]
    [SerializeField] private bool spawnOnStart = true;

    private bool _hudVisible = false;
    private ITimer _currentGameTimer;
    private float _nextTimerPoll = 0f;
    
    public static event Action<float> OnUpdateGameTimer = delegate {};
    
    [ContextMenu("Debug - Toggle Hud")]
    private void DebugToggleHud() {
        _hudVisible = !_hudVisible;
        SetHudEnabled(_hudVisible);
    }

    [ContextMenu("Debug - Level Up Player")]
    private void DebugLevelUp() {
        if (!Debug.isDebugBuild) return;
        if (timeManager.IsGamePaused()) return;
        globalVariableManager.DebugLevelUp();
    }

    private void SetHudEnabled(bool shouldDrawHud) {
        if(onToggleHudChannel) {
            onToggleHudChannel.RaiseEvent(shouldDrawHud);
        }
    }

    private void HandlePlayerDeath() {
        if (onDefeatChannel) {
            onDefeatChannel.RaiseEvent();
        }
    }

    private void EndGame() {
        _currentGameTimer.Stop();
        spawnManager.SetSpawning(false);
        spawnManager.Wipe();
        inputManager.SetPlayerInputEnabled(false);
        playerCharacter.SetActive(false);
        SetHudEnabled(false);
    }
    private void StartGame() {
        playerCharacter.SetActive(true);
        inputManager.SetPlayerInputEnabled(true);
        SetHudEnabled(true);
        if (spawnOnStart) {
            spawnManager.SetSpawning(true);
        }
        _currentGameTimer.Start();
        _nextTimerPoll = 0;
    }

    private void Update() {
        timeManager.Update();

        if (timeManager.IsGamePaused()) return;
        if (Time.time < _nextTimerPoll) return;
        Debug.Log("Poll!");
        _nextTimerPoll = Time.time + timerPollingInterval;
        OnUpdateGameTimer?.Invoke(_currentGameTimer.CurrentTime);
    }

    private void Awake() {
        Random.InitState(System.DateTime.Now.Millisecond);
        globalVariableManager.Init();
        weaponUpgradeManager.Init();
        _currentGameTimer = timeManager.CreateTimer();
        StartGame();
    }
    
    private void OnValidate() {
        globalVariableManager.ResetPlayerVariables();
    }

    private void OnEnable() {
        timeManager.OnEnable();
        weaponUpgradeManager.OnEnable();
        
        PlayerCharacter.OnPlayerDeath += HandlePlayerDeath;
        ExperienceCollectible.OnExperienceCollected += globalVariableManager.AddCurrentExperience;
        InputManager.OnDebugLevelUpInputPerformed += DebugLevelUp;
        
        if (onStartGameChannel) {
            onStartGameChannel.OnEventRaised += StartGame;
        }
        if (onEndGameChannel) {
            onEndGameChannel.OnEventRaised += EndGame;
        }
    }
    
    private void OnDisable() {
        timeManager.OnDisable();
        weaponUpgradeManager.OnDisable();
        
        PlayerCharacter.OnPlayerDeath -= HandlePlayerDeath;
        ExperienceCollectible.OnExperienceCollected -= globalVariableManager.AddCurrentExperience;
        InputManager.OnDebugLevelUpInputPerformed -= DebugLevelUp;
        
        if (onStartGameChannel) {
            onStartGameChannel.OnEventRaised -= StartGame;
        }
        if (onEndGameChannel) {
            onEndGameChannel.OnEventRaised -= EndGame;
        }
    }
}
