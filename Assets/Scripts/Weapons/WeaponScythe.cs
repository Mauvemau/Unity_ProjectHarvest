using UnityEngine;

public class WeaponScythe : Weapon {
    [Header("Weapon Specific Stats")]
    [SerializeField, Range(10f, 360f)] private float attackAngle = 90f;
    [SerializeField] private float pushForce = 12f;



}
