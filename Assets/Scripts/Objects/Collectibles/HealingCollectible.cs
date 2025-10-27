using System;
using UnityEngine;

public class HealingCollectible : Collectible {
    [Header("Collectible Specific Settings")]
    [Tooltip("Amount of health healed by this collectible when it's collected")]
    [SerializeField, Min(0)] private float healAmount = 5f;

    public static event Action<float> OnHealthPickup = delegate {};
    
    protected override void OnCollect() {
        if (!gameObject.activeInHierarchy) return;
        OnHealthPickup.Invoke(healAmount);
    }
}
