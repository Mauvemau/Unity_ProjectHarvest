using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Event Listeners")]
    [SerializeField] private AudioEventChannelSO onPlayAudioEventChannel;
    
    public static void PlayAudioEvent(AK.Wwise.Event audioEvent, GameObject parent){
        AkUnitySoundEngine.PostEvent(audioEvent.Id, parent);
    }
    
    private void PlayAudioEventLocal(AK.Wwise.Event audioEvent) {
        AkUnitySoundEngine.PostEvent(audioEvent.Id, gameObject);
    }

    private void OnEnable() {
        if (onPlayAudioEventChannel) {
            onPlayAudioEventChannel.OnEventRaised += PlayAudioEventLocal;
        }
    }

    private void OnDisable() {
        if (onPlayAudioEventChannel) {
            onPlayAudioEventChannel.OnEventRaised -= PlayAudioEventLocal;
        }
    }
}
