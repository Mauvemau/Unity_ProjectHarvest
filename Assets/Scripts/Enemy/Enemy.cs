using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable, IPushable {
    [Header("References")] 
    [SerializeField] private GameObject threatTargetReference;
    
    [Header("Health Settings")] 
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;

    [Header("Movement Settings")] 
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody2D _rb;
    private bool _alive;
    private Vector2 _pushVelocity;

    [ContextMenu("Debug - Revive")]
    public void Revive() {
        if (_alive) return;
        currentHealth = maxHealth;
        _alive = true;
    }
    
    [ContextMenu("Debug - Kill")]
    public void Kill() {
        if (!_alive) return;
        currentHealth = 0;
        _alive = false;
    }
    
    public void SetMaxHealth(float value) {
        if (value <= 0) {
            Debug.LogWarning($"{name}: Trying to set max health to a value less or equal to zero.");
        }

        float healthPercent = 0f;
        
        if (maxHealth > 0) {
            healthPercent = currentHealth / maxHealth;
        }

        maxHealth = value;
        currentHealth = healthPercent * maxHealth;
        
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0 && _alive) {
            Kill();
        }
    }
    
    public void SetCurrentHealth(float value) {
        if (!_alive) return;
        currentHealth = value;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;            
        }
        if (currentHealth <= 0) {
            Kill();
        }
    }
    
    public void Heal(float value) {
        SetCurrentHealth(currentHealth + value);
    }
    
    public void TakeDamage(float damage) {
        SetCurrentHealth(currentHealth - damage);
    }
    
    
    public void RequestPush(Vector2 direction, float force) {
        if(!_alive) return;
        if (!_rb) return;
        _pushVelocity = direction * force;
    }

    public void SetThreatTarget(GameObject target) {
        threatTargetReference = target;
    }
    
    //

    private void HandleTrigger(Collider2D col) {
        if (!_alive || !threatTargetReference) return;
        if(col.gameObject.TryGetComponent<PlayerCharacter>(out var player)) {
            Debug.Log($"{name}: Triggered enter player");
        }
    }
    
    private void HandleMovementBehaviour() {
        if (!_rb || !_alive || !threatTargetReference) return;

        Vector2 direction = (threatTargetReference.transform.position - transform.position).normalized;
        Vector2 move = direction * (movementSpeed * Time.fixedDeltaTime);
        
        Vector2 newPosition = _rb.position + move + _pushVelocity * Time.fixedDeltaTime;

        _rb.MovePosition(newPosition);
        
        _pushVelocity *= 0.9f;
    }
    
    private void BaseInit() {
        if (!TryGetComponent(out _rb)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_rb)}\"");
        }
        Revive();
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        HandleTrigger(collision);
    }
    
    private void FixedUpdate() {
        HandleMovementBehaviour();
    }
    
    private void Awake() {
        BaseInit();
    }
    
}
