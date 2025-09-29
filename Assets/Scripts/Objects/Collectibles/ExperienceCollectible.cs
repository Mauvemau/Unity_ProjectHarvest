using System;
using UnityEngine;

public class ExperienceCollectible : Collectible {
    [Header("Collectable Specific Settings")]
    [Tooltip("Amount of experience given by this collectible when it's collected")]
    [SerializeField, Min(0)] private float experienceAmount = 1f;

    public static event Action<float> OnExperienceCollected = delegate {};
    
    protected override void OnCollect() {
        if (!gameObject.activeInHierarchy) return;
        OnExperienceCollected.Invoke(experienceAmount);
    }
}
