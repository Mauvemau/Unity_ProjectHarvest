using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CustomCallback {
    [SerializeField] private UnityEvent unityEvent;
    
    public UnityEvent Event => unityEvent;
    
    public void Invoke() {
        unityEvent.Invoke();
    }
}
