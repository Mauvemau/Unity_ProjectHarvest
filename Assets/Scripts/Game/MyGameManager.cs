using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Global Variables")]
    [SerializeField] private GlobalVariableManager globalVariableManager;
    [Header("Extra Variables")]
    [Tooltip("Amount of money currently held by the player")]
    [SerializeField] private int coinsHeld;
    
    private void Awake() {
        globalVariableManager.Init();
    }
    
    private void OnValidate() {
        globalVariableManager.ResetPlayerVariables();
    }

    private void OnEnable() {
        ExperienceCollectible.OnExperienceCollected += globalVariableManager.AddCurrentExperience;
    }
}
