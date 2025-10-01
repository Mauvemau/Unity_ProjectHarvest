using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolHandler {
    [SerializeField] private MyObjectPool pool;
    [Header("Event Listeners")]
    [SerializeField] private Vector2EventChannelSo onRequestPoolChannel;
    [SerializeField] private VoidEventChannelSO onReturnPoolChannel;
    
    private void PerformPoolRequest(Vector2 position) {
        pool.PoolRequest(position, Quaternion.identity, Vector3.one);
    }

    private void PerformPoolReturn() {
        pool.PoolReturn();
    }

    public void PerformPoolCleanup() {
        pool.PoolCleanup();
    }
    
    public void OnEnable(GameObject parent) {
        pool.Initialize(parent);
        if (onRequestPoolChannel) {
            onRequestPoolChannel.onEventRaised += PerformPoolRequest;
        }
        if(onReturnPoolChannel) {
            onReturnPoolChannel.onEventRaised += PerformPoolReturn;
        }
    }

    public void OnDisable() {
        if (onRequestPoolChannel) {
            onRequestPoolChannel.onEventRaised -= PerformPoolRequest;
        }
        if(onReturnPoolChannel) {
            onReturnPoolChannel.onEventRaised -= PerformPoolReturn;
        }
    }
}

public class ObjectPoolManager : MonoBehaviour {
    [Header("Pool Settings")]
    [SerializeField] private List<PoolHandler> pools;
    
    [Header("Event Listeners")]
    [SerializeField] private VoidEventChannelSO onRequestResetAllPools;

    private void ResetAllPools() {
        foreach (PoolHandler pool in pools) {
            pool.PerformPoolCleanup();
        }
    }
    
    private void OnEnable() {
        foreach (PoolHandler pool in pools) {
            pool.OnEnable(gameObject);
        }

        if (onRequestResetAllPools) {
            onRequestResetAllPools.onEventRaised += ResetAllPools;
        }
    }

    private void OnDisable() {
        foreach (PoolHandler pool in pools) {
            pool.OnDisable();
        }
        
        if (onRequestResetAllPools) {
            onRequestResetAllPools.onEventRaised -= ResetAllPools;
        }
    }
}
