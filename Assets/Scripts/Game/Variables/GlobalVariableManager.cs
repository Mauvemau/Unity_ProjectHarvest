using UnityEngine;

[System.Serializable]
public class GlobalVariableManager {
    [Header("Global Game Variables")] 
    [SerializeField] private GameVariables gameBaseVariables;
    [SerializeField] private GameVariables gameCurrentVariables;
    
    [Header("Game Variables Settings")]
    [Tooltip("Each level earned, the experience needed to level up is multiplied by this amount")]
    [SerializeField] private float experienceNeededIncrease = 1.15f;

    [Header("Global Player Variables")] 
    [SerializeField] private PlayerVariables playerBaseVariables;
    [Tooltip("Multiplies base stats. (Permanent Upgrades)")]
    [SerializeField] private PlayerVariables playerVariableMultiplier;
    [SerializeField, ReadOnly] private PlayerVariables playerCurrentVariables;

    [Header("Event Invokers")]
    [SerializeField] private ProgressBarController xpBarController;
    [SerializeField] private StringEventChannelSO onUpdateLevelValue;

    private void CheckIfLevelUp() {
        while (gameCurrentVariables.CurrentExperience >= gameCurrentVariables.ExperienceNeeded) {
            gameCurrentVariables.CurrentExperience -= gameCurrentVariables.ExperienceNeeded;
            gameCurrentVariables.CurrentLevel++;
            gameCurrentVariables.ExperienceNeeded *= experienceNeededIncrease;

            if (onUpdateLevelValue) {
                onUpdateLevelValue.RaiseEvent($"{gameCurrentVariables.CurrentLevel}");
            }
        }
    }
    
    public void AddCurrentExperience(float amount) {
        gameCurrentVariables.CurrentExperience += amount;
        CheckIfLevelUp();
        xpBarController.UpdateValues(gameCurrentVariables.CurrentExperience, gameCurrentVariables.ExperienceNeeded);
    }
    
    //
    
    [ContextMenu("Debug GlobalVars - Reset XP")]
    public void ResetGameVariables() {
        gameCurrentVariables = gameBaseVariables;
    }
    
    [ContextMenu("Debug GlobalVars - Reset Player Stats")]
    public void ResetPlayerVariables() {
        playerCurrentVariables = playerBaseVariables * playerVariableMultiplier;
    }
    
    [ContextMenu("Debug GlobalVars - Reset All")]
    public void ResetAll() {
        ResetPlayerVariables();
        ResetGameVariables();
    }
    
    public void Init() {
        ResetAll();
    }
}
