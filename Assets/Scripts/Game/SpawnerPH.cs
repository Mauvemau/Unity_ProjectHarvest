using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerPh : MonoBehaviour {
    [Header("Pool Settings")]
    [SerializeField] private MyObjectPool spawnPool;
    
    [Header("Spawning Settings")]
    [SerializeField] private bool spawn = true;
    [SerializeField, Min(0.1f)] private float maxSpawnDistance = 5f;
    [SerializeField, Min(0)] private float spawnInterval = 1f;
    
    private float _nextSpawn = 0f;
    
    private void Awake() {
        spawnPool.Initialize(gameObject);
    }

    private void Update() {
        if (!spawn) return;
        if (_nextSpawn > Time.time) return;
        _nextSpawn = Time.time + spawnInterval;
        
        float x = Random.Range(-maxSpawnDistance, maxSpawnDistance);
        float y = Random.Range(-maxSpawnDistance, maxSpawnDistance);
        Vector3 randomOffset = new Vector3(x, y, 0f);
        Vector3 spawnPos = transform.position + randomOffset;
        
        spawnPool.PoolRequest(spawnPos, Quaternion.identity, Vector3.one);
    }
}
