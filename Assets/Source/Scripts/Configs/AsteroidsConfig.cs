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
    }
}