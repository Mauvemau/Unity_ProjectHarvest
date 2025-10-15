using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private PlayerCharacter playerCharacterReference;
    
    [Header("Event Listeners")] 
    [SerializeField] private VoidEventChannelSO onRequestReviveCharacter;

    private void MoveCharacter(Vector2 direction) {
        playerCharacterReference?.RequestMovement(direction);
    }

    private void PerformCharacterInteraction() {
        Debug.Log("Interacted!");
    }

    private void Awake() {
        if (!playerCharacterReference) {
            Debug.LogWarning($"{name}: missing reference \"{nameof(playerCharacterReference)}\"");
        }
    }
    
    private void OnEnable() {
        InputManager.OnPlayerMoveInputPerformed += MoveCharacter;
        InputManager.OnPlayerInteractInputPerformed += PerformCharacterInteraction;
        
        if (onRequestReviveCharacter) {
            onRequestReviveCharacter.OnEventRaised += playerCharacterReference.Revive;
        }
    }

    private void OnDisable() {
        InputManager.OnPlayerMoveInputPerformed -= MoveCharacter;
        InputManager.OnPlayerInteractInputPerformed -= PerformCharacterInteraction;
        
        if (onRequestReviveCharacter) {
            onRequestReviveCharacter.OnEventRaised -= playerCharacterReference.Revive;
        }
    }
}
