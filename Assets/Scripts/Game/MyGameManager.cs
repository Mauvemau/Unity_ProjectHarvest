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

    [Header("Time Controller")] 
    [SerializeField] private MyGameTimeController timeController;

    [Header("Event Invokers")] 
    [SerializeField] private VoidEventChannelSO onDefeatChannel;
    [SerializeField] private BoolEventChannelSO onToggleHudChannel;

    [Header("Event Listeners")]
    [SerializeField] private VoidEventChannelSO onStartGameChannel;
    [SerializeField] private VoidEventChannelSO onEndGameChannel;
    
    [Header("Debug Controls")]
    [SerializeField] private bool spawnOnStart = true;

    private bool _hudVisible = false;

    [ContextMenu("Debug - Toggle Hud")]
    private void DebugToggleHud() {
        _hudVisible = !_hudVisible;
        SetHudEnabled(_hudVisible);
    }

    [ContextMenu("Debug - Level Up Player")]
    private void DebugLevelUp() {
        if (!Debug.isDebugBuild) return;
        if (timeController.IsGamePaused()) return;
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
    }

    private void Awake() {
        Random.InitState(System.DateTime.Now.Millisecond);
        globalVariableManager.Init();
        weaponUpgradeManager.Init();
        StartGame();
    }
    
    private void OnValidate() {
        globalVariableManager.ResetPlayerVariables();
    }

    private void OnEnable() {
        timeController.OnEnable();
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
        timeController.OnDisable();
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
