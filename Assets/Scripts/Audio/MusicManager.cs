using UnityEngine;

public class MusicManager : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private AK.Wwise.Event startAudioEvent;

    [Header("Settings")] 
    [SerializeField] private bool autoplayOnStart = false;

    private void Start() {
        if (!autoplayOnStart) return;
        startAudioEvent.Post(gameObject);
    }
}
