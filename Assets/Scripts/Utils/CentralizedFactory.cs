using UnityEngine;

/// <summary>
/// A centralized factory that also pools objects
/// </summary>
public class CentralizedFactory : MonoBehaviour {
    [Header("Pooling")]
    [SerializeField] private MyObjectPool[] objectPools;

    public GameObject Create(GameObject prefabToCreate, Vector3 position, Quaternion rotation, Vector3 scale) {
        if(!prefabToCreate) {
            Debug.LogWarning($"{name}: trying to create null object!");
        }
        foreach (MyObjectPool pool in objectPools) {
           if(pool.GetObjectToPool() == prefabToCreate && pool.GetPooledCount() > 0) {
                return pool.PoolGetRequest(position, rotation, scale);
           }
        }
        
        Debug.LogWarning($"{name}: object not pooled, instantiating it instead");
        GameObject obj = Object.Instantiate(prefabToCreate, transform);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;
        return obj;
    }

    private void Awake() {
        ServiceLocator.SetService(this);
        foreach (MyObjectPool pool in objectPools) {
            pool.Initialize(gameObject);
        }
    }
}
