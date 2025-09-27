using UnityEngine;

public class ExperienceCollectible : Collectible {
    [Header("Collectable Specific Settings")]
    [SerializeField] private float experienceAmount = 1f;

    protected override void OnCollect() {
        if (!gameObject.activeInHierarchy) return;
        Debug.Log($"+{experienceAmount} XP gained!");
    }
}
