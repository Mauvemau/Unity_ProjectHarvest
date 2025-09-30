using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {

    [FormerlySerializedAs("_playerCharacterReference")]
    [Header("References")] 
    [SerializeField] private PlayerCharacter playerCharacterReference;

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
    }

    private void OnDisable() {
        InputManager.OnPlayerMoveInputPerformed -= MoveCharacter;
        InputManager.OnPlayerInteractInputPerformed -= PerformCharacterInteraction;
    }
}
