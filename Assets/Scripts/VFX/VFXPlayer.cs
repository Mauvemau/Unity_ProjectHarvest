using UnityEngine;

[System.Serializable]
public class VFXPlayer {
    [SerializeField] private Factory vfxFactory;
    [SerializeField] private VFXPreset vfxPreset;

    [Header("Offset Settings")]
    [SerializeField] private Vector3 scaleOffset = Vector3.one;

    public void PlayVFX(Vector3 position, Quaternion rotation, float duration = 0f) {
        GameObject vfxObject = vfxFactory.Create(position, rotation, Vector3.one);
        if (!vfxObject || !vfxObject.TryGetComponent(out VFX vfx)) return;

        vfxObject.transform.localScale = Vector3.Scale(vfxObject.transform.localScale, scaleOffset);

        vfx.Play(vfxPreset, duration);
    }
}
