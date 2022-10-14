using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Asteroids Config", menuName = "Configs/Asteroids", order = 0)]
    public class AsteroidsConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxSize { get; private set; } = 8;
        [field: SerializeField] public float MinSize { get; private set; } = 2;

        [field: Header("Smaller asteroids gives more points! It is linear")]
        [field: SerializeField] public int MinReward { get; private set; } = 50;

        [field: SerializeField] public int MaxReward { get; private set; } = 350;

        [field: Space] [field: Header("Spawn Properties")]
        [field: SerializeField] public float SpawnTick { get; private set; } = 6;

        [field: SerializeField, Range(1,3)] public float SplitAsteroidsVelocityMultiplier { get; private set; } = 1.4f;
        [field: SerializeField] public float MaxAmount { get; private set; } = 6;
    }
}