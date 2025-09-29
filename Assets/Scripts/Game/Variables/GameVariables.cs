using UnityEngine;

[System.Serializable]
public class GameVariables {
    [field:SerializeField] public float CurrentLevel { get; set; }
    [field:SerializeField] public float ExperienceNeeded { get; set; }
    [field:SerializeField] public float CurrentExperience { get; set; }
}
