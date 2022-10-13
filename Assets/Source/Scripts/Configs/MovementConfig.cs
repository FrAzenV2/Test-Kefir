using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Movement Config", menuName = "Configs/Movement", order = 0)]
    public class MovementConfig : ScriptableObject
    {

        [field: SerializeField] public float MinVelocity { get; private set; } = 0;
        [field: SerializeField] public float MaxVelocity { get; private set; } = 30;
        [field: SerializeField] public Vector2 HorizontalBoundaries { get; private set; } = new(-9, 9);
        [field: SerializeField] public Vector2 VerticalBoundaries { get; private set; } = new(-5, 5);

        [field: SerializeField] public float Acceleration { get; private set; } = 10;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 180;
    }
}