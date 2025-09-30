using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IMovable, IDamageable, IPushable {
    [Header("References")] 
    [Tooltip("The entity to target for behaviour")]
    [SerializeField] private GameObject threatTargetReference;
    [Tooltip("A health bar will be updated based on the entity's health values if set")]
    [SerializeField] private HealthBar healthBarReference;
    
    [Header("Health Settings")] 
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;

    [Header("Movement Settings")] 
    [SerializeField] private float movementSpeed = 1f;

    [Header("Drops Settings")] 
    [SerializeField] private DropManager dropManager;
    
    [Header("General Settings")] 
    [Tooltip("Finds a PlayerCharacter and assigns it as threat target reference automatically")]
    [SerializeField] private bool findPlayer = false;
    [Tooltip("The GameObject is disabled if currentHealth reaches 0")]
    [SerializeField] private bool disableOnDeath = false;
    
    private Rigidbody2D _rb;
    private bool _alive;
    private Vector2 _movementDirection;
    private Vector2 _pushVelocity;

    [ContextMenu("Debug - Revive")]
    public void Revive() {
        if (_alive) return;
        currentHealth = maxHealth;
        _alive = true;
        UpdateHealthBar();
    }
    
    [ContextMenu("Debug - Kill")]
    public void Kill() {
        if (!_alive) return;
        currentHealth = 0;
        _alive = false;
        
        dropManager.HandleRequestDrops(transform.position);
        
        if (disableOnDeath) {
            gameObject.SetActive(false);
        }
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
        
        UpdateHealthBar();
        
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

        UpdateHealthBar();
        
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

    public void RequestMovement(Vector2 direction) {
        Debug.LogWarning($"{name}: trying to request movement on an entity that can't be externally moved!");
    }

    public Vector2 GetMovementDirection() {
        return _movementDirection.normalized;
    }

    [ContextMenu("Debug - Find Player")]
    private void TryFindThreatTarget() {
        if (!findPlayer || !ServiceLocator.TryGetService(out PlayerCharacter player)) return;
        if (player == null) {
            Debug.LogWarning($"{name}: Unable to find threat target!");
        }
        else {
            threatTargetReference = player.gameObject;
        }
    }
    
    public void SetThreatTarget(GameObject target) {
        threatTargetReference = target;
    }
    
    //

    private void UpdateHealthBar() {
        if (!healthBarReference) return;
        healthBarReference.SetMaxValue(maxHealth);
        healthBarReference.SetCurrentValue(currentHealth);
        healthBarReference.gameObject.SetActive(currentHealth < maxHealth);
    }
    
    private void HandleMovementBehaviour() {
        if (!_rb || !_alive || !threatTargetReference) return;

        _movementDirection = (threatTargetReference.transform.position - transform.position).normalized;
        
        Vector2 move = _movementDirection * (movementSpeed * Time.fixedDeltaTime);
        Vector2 newPosition = _rb.position + move + _pushVelocity * Time.fixedDeltaTime;

        _rb.MovePosition(newPosition);
        
        _pushVelocity *= 0.9f;
    }
    
    private void BaseInit() {
        _alive = false;
        Revive();
    }

    private void FixedUpdate() {
        HandleMovementBehaviour();
    }
    
    private void Awake() {
        if (!TryGetComponent(out _rb)) {
            Debug.LogError($"{name}: missing reference \"{nameof(_rb)}\"");
        }
        BaseInit();
    }

    private void OnEnable() {
        Revive();
        TryFindThreatTarget();
    }
}
