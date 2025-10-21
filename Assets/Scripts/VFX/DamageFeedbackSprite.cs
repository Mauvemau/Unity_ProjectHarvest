using UnityEngine;

[System.Serializable]
public class DamageFeedbackSprite {
    [SerializeField] private float feedbackDuration = .5f;
    
    [Header("Color Settings")]
    [SerializeField] private Color damageTint = Color.red;
    [SerializeField] private Color healingTint = Color.green;
    [SerializeField] private AnimationCurve colorSwitchCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private Color _defaultColor;
    private Color _targetColor;
    private float _elapsed;
    private bool _isPlaying;
    
    private SpriteRenderer _spriteRenderer;

    public void Reset() {
        _isPlaying = false;
        _elapsed = 0;

        if (!_spriteRenderer) return;
        _spriteRenderer.color = _defaultColor;
    }
    
    public void PlayDamageFeedback(float damageReceived) {
        _targetColor = damageReceived > 0 ? damageTint : healingTint;
        _elapsed = 0f;
        _isPlaying = true;
    }
    
    public void Update() {
        if (!_isPlaying || !_spriteRenderer) return;

        _elapsed += Time.deltaTime;
        float t = _elapsed / feedbackDuration;

        if (t >= 1f) {
            _spriteRenderer.color = _defaultColor;
            _isPlaying = false;
            return;
        }

        float curveValue = t < 0.5f ? colorSwitchCurve.Evaluate(t * 2f) : colorSwitchCurve.Evaluate((1f - t) * 2f);

        _spriteRenderer.color = Color.Lerp(_defaultColor, _targetColor, curveValue);
    }

    public void Init(SpriteRenderer spriteRenderer) {
        if (!spriteRenderer) return;
        _spriteRenderer = spriteRenderer;
        _defaultColor = spriteRenderer.color;
        _elapsed = 0;
        _isPlaying = false;
    }
}
