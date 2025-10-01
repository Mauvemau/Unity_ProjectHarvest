using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    [Header("Global Variables")]
    [SerializeField] private GlobalVariableManager globalVariableManager;
    [Header("Extra Variables")]
    [Tooltip("Amount of money currently held by the player")]
    [SerializeField] private int coinsHeld;

    [Header("Time Controller")] 
    [SerializeField] private MyGameTimeController timeController;

    [Header("Event Invokers")] 
    [SerializeField] private VoidEventChannelSO onDefeatChannel;

    private void HandlePlayerDeath() {
        if (onDefeatChannel) {
            onDefeatChannel.RaiseEvent();
        }
    }
    
    private void Awake() {
        Random.InitState(System.DateTime.Now.Millisecond);
        globalVariableManager.Init();
    }
    
    private void OnValidate() {
        globalVariableManager.ResetPlayerVariables();
    }

    private void OnEnable() {
        timeController.OnEnable();
        PlayerCharacter.OnPlayerDeath += HandlePlayerDeath;
        ExperienceCollectible.OnExperienceCollected += globalVariableManager.AddCurrentExperience;
    }
    
    private void OnDisable() {
        timeController.OnDisable();
        PlayerCharacter.OnPlayerDeath -= HandlePlayerDeath;
        ExperienceCollectible.OnExperienceCollected -= globalVariableManager.AddCurrentExperience;
    }
}
