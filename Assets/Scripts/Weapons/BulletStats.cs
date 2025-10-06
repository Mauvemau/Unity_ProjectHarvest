using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

[System.Serializable]
public class BulletStats {
    //Speed at which the bullet travels
    [SerializeField] public float speed = 100f;
    // Amount of time in seconds a bullet stays alive
    [SerializeField] public float lifeTime = 5f;
    // Amount of targets a bullet can penetrate trough
    [SerializeField] public int penetrationCount = 0;
}
