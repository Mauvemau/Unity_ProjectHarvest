using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player Character Reference here
    // Player Weapon Reference here

    private void MoveCharacter(Vector2 direction) {

    }

    private void AimWeapon(Vector2 direction) {

    }

    private void PerformCharacterInteraction() {
        Debug.Log("Interacted!");
    }

    private void OnEnable() {
        InputManager.OnPlayerMoveInputPerformed += MoveCharacter;
        InputManager.OnPlayerAimInputPerformed += AimWeapon;
        InputManager.OnPlayerInteractInputPerformed += PerformCharacterInteraction;
    }

    private void OnDisable() {
        InputManager.OnPlayerMoveInputPerformed -= MoveCharacter;
        InputManager.OnPlayerAimInputPerformed -= AimWeapon;
        InputManager.OnPlayerInteractInputPerformed -= PerformCharacterInteraction;
    }

}
