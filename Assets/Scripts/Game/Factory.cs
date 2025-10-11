using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory {
    [SerializeField] private GameObject prefabToCreate;
    [Header("Optional References")]
    [SerializeField] private CentralizedFactory centralizedFactory;
    [SerializeField] private bool autoFindCentralizedFactory;
    
    [Header("Debug")]
    [SerializeField, ReadOnly] private List<GameObject> creations;

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
        creations.Add(obj);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;
        return obj;
    }

    public void Destroy(GameObject objReference) {
        if (objReference == null) return;
        if (!creations.Contains(objReference)) return;
        creations.Remove(objReference);
        Object.Destroy(objReference);
    }
    
    public void SoftDestroy(GameObject objReference) {
        if (!objReference) return;
        if (!creations.Contains(objReference)) return;
        objReference.SetActive(false);
    }

    public void SetPrefabToCreate(GameObject prefab) {
        prefabToCreate = prefab;
    }
    
    public void SoftWipe() {
        foreach (GameObject creation in creations) {
            if (creation && creation.activeInHierarchy) {
                creation.SetActive(false);
            }
        }
    }
    
    public void HardWipe() {
        for (int i = creations.Count - 1; i >= 0; i--) {
            GameObject creation = creations[i];
            if (creation) {
                Object.Destroy(creation);
            }
        }
        creations.Clear();
    }
}
