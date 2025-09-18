using UnityEngine;

public class Weapon : MonoBehaviour {
    [Header("Base Settings")]
    [SerializeField] private LayerMask targetLayer;

    protected Vector2 _aimDirection;
}
