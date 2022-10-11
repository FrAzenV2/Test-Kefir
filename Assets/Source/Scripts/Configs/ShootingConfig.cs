using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Shooting Config", menuName = "Configs/ Shooting", order = 0)]
    public class ShootingConfig : ScriptableObject
    {
        [field: SerializeField] public float BulletSpeed { get; private set; } = 10;
        [field: SerializeField] public float BulletLifetime { get; private set; } = 3;
        [field: SerializeField] public float BulletShotCooldown { get; private set; } = 0.3f;
    }
}