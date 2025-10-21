using UnityEngine;

public class AnimationControllerPH : MonoBehaviour {
    [Header("References")]
    [SerializeField] private SpriteRenderer[] spriteRendererReferences;
    [Header("Settings")]
    [SerializeField] private float spinInterval = .5f;
    [SerializeField] private bool flipPositionX = true;

    private float _nextSpin = 0;
    
    private void Update() {
        if (spriteRendererReferences.Length <= 0) return;
        if(Time.time < _nextSpin) return;
        _nextSpin = Time.time + spinInterval;

        foreach (SpriteRenderer sr in spriteRendererReferences) {
            if (!sr) continue;
            
            sr.flipX = !sr.flipX;

            if (!flipPositionX) continue;
            Transform t = sr.transform;
            Vector3 localPos = t.localPosition;
            localPos.x = -localPos.x;
            t.localPosition = localPos;
        }
    }
}
