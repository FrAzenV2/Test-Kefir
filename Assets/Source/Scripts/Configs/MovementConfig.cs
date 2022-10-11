using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Movement Config", menuName = "Configs/Movement", order = 0)]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] public float Acceleration { get; private set; } = 10;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 180;
    }
}