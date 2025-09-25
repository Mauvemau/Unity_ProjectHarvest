using UnityEngine;

public class ExperienceCollectable : Collectable {
    [Header("Collectable Specific Settings")]
    [SerializeField] private float experienceAmount = 1f;

    protected override void OnCollect() {
        if (!gameObject.activeInHierarchy) return;
        Debug.Log($"+{experienceAmount} XP gained!");
    }
}
