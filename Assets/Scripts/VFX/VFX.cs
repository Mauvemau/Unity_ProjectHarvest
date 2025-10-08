using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class VFX : MonoBehaviour, IVFX {
    private Animator _animator;
    private float _clipLength;

    private void FinishAnimation() {
        gameObject.SetActive(false);
    }

    public void Play(VFXPreset preset) {
        if (!preset.Controller) {
            FinishAnimation();
            return;
        }
        _animator.runtimeAnimatorController = preset.Controller;

        _animator.speed = preset.PlaybackSpeed;
        _animator.Play(0, 0, preset.AnimationStartOffset);

        AnimatorClipInfo[] info = _animator.GetCurrentAnimatorClipInfo(0);
        _clipLength = info.Length > 0 ? info[0].clip.length : 0.5f;

        Invoke(nameof(FinishAnimation), _clipLength / _animator.speed);
    }

    private void Awake() {
        if (!TryGetComponent(out _animator)) {
            Debug.LogError($"{name}: missing required component {nameof(Animator)}");
        }
    }

    private void OnDisable() {
        CancelInvoke(nameof(FinishAnimation));
    }
}