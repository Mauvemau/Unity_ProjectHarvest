using UnityEngine;

public class ButtonAudioFeedback : ButtonFeedback {
    [Header("Audio Events")] 
    [SerializeField] private AK.Wwise.Event selectAudioEvent;
    [SerializeField] private AK.Wwise.Event clickAudioEvent;
    
    protected override void OnButtonSelected() {
        AudioProxy.PostEvent(selectAudioEvent);
    }
    protected override void OnButtonClicked() {
        AudioProxy.PostEvent(clickAudioEvent);
    }
}
