using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    [Header("Global Variables")]
    [SerializeField] private GlobalVariableManager globalVariableManager;
    [Header("Extra Variables")]
    [Tooltip("Amount of money currently held by the player")]
    [SerializeField] private int coinsHeld;

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
    private bool skipMainMenu = false;

    private bool _hudVisible = false;

    [ContextMenu("Debug - ToggleHud")]
    private void DebugToggleHud() {
        _hudVisible = !_hudVisible;
        SetHudEnabled(_hudVisible);
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
        spawnManager.SetSpawning(true);
    }

    private void Awake() {
        Random.InitState(System.DateTime.Now.Millisecond);
        globalVariableManager.Init();
        StartGame();
    }
    
    private void OnValidate() {
        globalVariableManager.ResetPlayerVariables();
    }

    private void OnEnable() {
        timeController.OnEnable();
        PlayerCharacter.OnPlayerDeath += HandlePlayerDeath;
        ExperienceCollectible.OnExperienceCollected += globalVariableManager.AddCurrentExperience;
        if (onStartGameChannel) {
            onStartGameChannel.onEventRaised += StartGame;
        }
        if (onEndGameChannel) {
            onEndGameChannel.onEventRaised += EndGame;
        }
    }
    
    private void OnDisable() {
        timeController.OnDisable();
        PlayerCharacter.OnPlayerDeath -= HandlePlayerDeath;
        ExperienceCollectible.OnExperienceCollected -= globalVariableManager.AddCurrentExperience;
        if (onStartGameChannel) {
            onStartGameChannel.onEventRaised -= StartGame;
        }
        if (onEndGameChannel) {
            onEndGameChannel.onEventRaised -= EndGame;
        }
    }
}
