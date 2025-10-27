using UnityEngine;

public class AudioProxy : MonoBehaviour {
    private static GameObject _instance;

    public static GameObject Instance => _instance;

    public static void PostEvent(AK.Wwise.Event audioEvent) {
        if (!Instance) return;
        if (string.IsNullOrEmpty(audioEvent.Name)) return;
        AkUnitySoundEngine.PostEvent(audioEvent.Name, Instance);
    }
    
    private void Awake() {
        _instance = gameObject;
        DontDestroyOnLoad(gameObject);
        AkUnitySoundEngine.RegisterGameObj(gameObject);
    }
    
    private void OnDestroy() {
        AkUnitySoundEngine.UnregisterGameObj(gameObject);
    }
}
