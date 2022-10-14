using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Shooting Config", menuName = "Configs/ Shooting", order = 0)]
    public class ShootingConfig : ScriptableObject
    {
        [field: SerializeField] public float BulletLifetime { get; private set; } = 3;
        [field: SerializeField] public float BulletShotCooldown { get; private set; } = 0.3f;
        [field: SerializeField] public float LaserLifetime { get; private set; } = 0.2f;

        [field: SerializeField] public int MaxLasers { get; private set; } = 3;

        [field: SerializeField] public float LaserCooldown { get; private set; } = 2;
    }
}