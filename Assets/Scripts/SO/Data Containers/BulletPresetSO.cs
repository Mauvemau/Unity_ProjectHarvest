using UnityEngine;

/// <summary>
/// Contains a preset that will define the behaviour and appareance of a bullet
/// </summary>
[CreateAssetMenu(menuName = "Containers/Bullet Preset")]
public class BulletPresetSO : ScriptableObject {
    [Header("Behaviour Settings")]
    [SerializeReference, SubclassSelector] private IBulletStrategy behaviour = new LinearShotStrategy();

    [Header("Visual Settings")]
    [SerializeField] private Sprite bulletSprite;

    public IBulletStrategy BehaviourHandler => behaviour;
    public Sprite BulletSprite => bulletSprite;
}
