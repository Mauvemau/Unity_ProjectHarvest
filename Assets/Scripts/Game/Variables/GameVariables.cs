using UnityEngine;

[System.Serializable]
public class GameVariables {
    [field:SerializeField] public float CurrentLevel { get; set; }
    [field:SerializeField] public float ExperienceNeeded { get; set; }
    [field:SerializeField] public float CurrentExperience { get; set; }
    
    public GameVariables Copy() {
        return new GameVariables {
            CurrentLevel = this.CurrentLevel,
            ExperienceNeeded = this.ExperienceNeeded,
            CurrentExperience = this.CurrentExperience
        };
    }
}
