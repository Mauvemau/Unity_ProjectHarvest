using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory {
    [SerializeField] private GameObject prefabToCreate;
    [Header("Optional References")]
    [SerializeField] private CentralizedFactory centralizedFactory;
    [SerializeField] private bool autoFindCentralizedFactory;

    private List<GameObject> _creations;

    private void TryFindCentralizedFactory() {
        if (centralizedFactory) return;
        if (ServiceLocator.TryGetService(out centralizedFactory)) {
            autoFindCentralizedFactory = false;
        }
    }

    public GameObject Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null) {
        if (!prefabToCreate) return null;
        if (autoFindCentralizedFactory) {
            TryFindCentralizedFactory();
        }
        if (centralizedFactory) {
            return centralizedFactory.Create(prefabToCreate, position, rotation, scale);
        }

        GameObject obj = Object.Instantiate(prefabToCreate, parent);
        _creations.Add(obj);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;
        return obj;
    }

    public void SoftWipe() {
        foreach (GameObject creation in _creations) {
            if (creation) {
                creation.SetActive(false);
            }
        }
    }
}
