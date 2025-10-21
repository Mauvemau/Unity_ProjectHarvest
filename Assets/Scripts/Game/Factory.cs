using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory {
    [SerializeField] private GameObject prefabToCreate;
    [Header("Optional References")]
    [SerializeField] private CentralizedFactory centralizedFactory;
    [SerializeField] private bool autoFindCentralizedFactory;
    
    private List<GameObject> _creations = new List<GameObject>();

    private void TryFindCentralizedFactory() {
        if (centralizedFactory) return;
        if (ServiceLocator.TryGetService(out centralizedFactory)) {
            autoFindCentralizedFactory = false;
        }
    }
    
    public GameObject Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null) {
        if (!prefabToCreate) return null;
        GameObject obj;
        
        if (autoFindCentralizedFactory) {
            TryFindCentralizedFactory();
        }
        if (centralizedFactory) {
            obj = centralizedFactory.Create(prefabToCreate, position, rotation, scale);
            _creations.Add(obj);
            return obj;
        }

        obj = Object.Instantiate(prefabToCreate, parent);
        _creations.Add(obj);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;
        return obj;
    }

    public void Destroy(GameObject objReference) {
        if (objReference == null) return;
        if (!_creations.Contains(objReference)) return;
        _creations.Remove(objReference);
        Object.Destroy(objReference);
    }
    
    public void SoftDestroy(GameObject objReference) {
        if (!objReference) return;
        if (!_creations.Contains(objReference)) return;
        objReference.SetActive(false);
    }

    public void SetPrefabToCreate(GameObject prefab) {
        prefabToCreate = prefab;
    }
    
    public void SoftWipe() {
        foreach (GameObject creation in _creations) {
            if (creation && creation.activeInHierarchy) {
                creation.SetActive(false);
            }
        }
    }
    
    public void HardWipe() {
        for (int i = _creations.Count - 1; i >= 0; i--) {
            GameObject creation = _creations[i];
            if (creation) {
                Object.Destroy(creation);
            }
        }
        _creations.Clear();
    }
}
