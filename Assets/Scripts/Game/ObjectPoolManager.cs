using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolHandler {
    [SerializeField] private MyObjectPool pool;
    [Header("Event Listeners")]
    [SerializeField] private Vector2EventChannelSO onRequestPoolChannel;
    [SerializeField] private VoidEventChannelSO onReturnPoolChannel;
    
    private void PerformPoolRequest(Vector2 position) {
        pool.PoolRequest(position, Quaternion.identity, Vector3.one);
    }

    private void PerformPoolReturn() {
        pool.PoolReturn();
    }
    
    public void OnEnable(GameObject parent) {
        pool.Initialize(parent);
        if (onRequestPoolChannel) {
            onRequestPoolChannel.OnEventRaised += PerformPoolRequest;
        }
        if(onReturnPoolChannel) {
            onReturnPoolChannel.OnEventRaised += PerformPoolReturn;
        }
    }

    public void OnDisable() {
        if (onRequestPoolChannel) {
            onRequestPoolChannel.OnEventRaised -= PerformPoolRequest;
        }
        if(onReturnPoolChannel) {
            onReturnPoolChannel.OnEventRaised -= PerformPoolReturn;
        }
    }
}

public class ObjectPoolManager : MonoBehaviour {
    [SerializeField] private List<PoolHandler> pools;
    
    private void OnEnable() {
        foreach (PoolHandler pool in pools) {
            pool.OnEnable(gameObject);
        }
    }

    private void OnDisable() {
        foreach (PoolHandler pool in pools) {
            pool.OnDisable();
        }
    }
}
