using UnityEngine;

[System.Serializable]
public class Factory {
    [Header("Object Settings")]
    [SerializeField] private GameObject prefabToCreate;
    [Header("Optional References")]
    [SerializeField] private CentralizedFactory centralizedFactory;

    public GameObject Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null) {
        if (centralizedFactory != null) {
            return centralizedFactory.Create(prefabToCreate, position, rotation, scale);
        }

        GameObject obj = Object.Instantiate(prefabToCreate, parent);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;
        return obj;
    }
}
