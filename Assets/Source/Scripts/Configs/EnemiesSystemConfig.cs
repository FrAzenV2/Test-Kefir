using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Enemies System Config", menuName = "Configs/Enemies", order = 0)]
    public class EnemiesSystemConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxEnemiesPerSpawn { get; private set; } = 3;
        [field: SerializeField] public int MinEnemiesPerSpawn { get; private set; } = 1;

        [field: Header("Cooldown Starts when all enemies are dead")]
        [field: SerializeField] public float SpawnCooldown { get; private set; } = 30;
    }
}