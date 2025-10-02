using UnityEngine;

public class SpawnerPh : MonoBehaviour {
    [Header("Factory Settings")]
    [SerializeField] private Factory enemyFactory;
    
    [Header("Spawning Settings")]
    [SerializeField] private bool spawn = true;
    
    [Header("Camera-Based Spawn Logic Settings")]
    [SerializeField] private Camera mainCameraReference; 
    [SerializeField, Min(0.1f)] private float spawnOffset = 1f; 
    
    [Header("Random Spawn Logic Settings")]
    [SerializeField, Min(0.1f)] private float maxSpawnDistance = 5f;
    [SerializeField, Min(0)] private float spawnInterval = 1f;
    
    private float _nextSpawn = 3f;

    /// <summary>
    /// Spawns within a square range around the gameObject position
    /// </summary>
    private Vector3 GetRandomSpawnPosition() {
        float x = Random.Range(-maxSpawnDistance, maxSpawnDistance);
        float y = Random.Range(-maxSpawnDistance, maxSpawnDistance);
        return transform.position + new Vector3(x, y, 0f);
    }

    /// <summary>
    /// Camera-aware mode: spawns at random positions on the rectangle edges of the camera view
    /// </summary>
    private Vector3 GetSpawnPositionFromCamera() {
        if (!mainCameraReference || !mainCameraReference.orthographic) {
            return GetRandomSpawnPosition();
        }
        
        float camHeight = mainCameraReference.orthographicSize * 2f;
        float camWidth = camHeight * mainCameraReference.aspect;

        Vector3 camCenter = mainCameraReference.transform.position;
        float halfWidth = camWidth / 2f;
        float halfHeight = camHeight / 2f;
        
        int side = Random.Range(0, 4);
        float x = 0f, y = 0f;

        switch (side) {
            case 0: // left
                x = camCenter.x - halfWidth - spawnOffset;
                y = Random.Range(camCenter.y - halfHeight, camCenter.y + halfHeight);
                break;
            case 1: // right
                x = camCenter.x + halfWidth + spawnOffset;
                y = Random.Range(camCenter.y - halfHeight, camCenter.y + halfHeight);
                break;
            case 2: // top
                y = camCenter.y + halfHeight + spawnOffset;
                x = Random.Range(camCenter.x - halfWidth, camCenter.x + halfWidth);
                break;
            case 3: // bottom
                y = camCenter.y - halfHeight - spawnOffset;
                x = Random.Range(camCenter.x - halfWidth, camCenter.x + halfWidth);
                break;
        }

        return new Vector3(x, y, 0f);
    }
    
    private void Update() {
        if (!spawn) return;
        if (_nextSpawn > Time.time) return;
        _nextSpawn = Time.time + spawnInterval;

        Vector3 spawnPos = GetSpawnPositionFromCamera();
        
        enemyFactory.Create(spawnPos, Quaternion.identity, Vector3.one);
    }
}
