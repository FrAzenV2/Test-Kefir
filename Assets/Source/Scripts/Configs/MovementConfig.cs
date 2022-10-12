using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Default Movement Config", menuName = "Configs/Movement", order = 0)]
    public class MovementConfig : ScriptableObject
    {

        [field: SerializeField] public Vector2 HorizontalBoundaries { get; private set; } = new Vector2(-15, 15);
        [field: SerializeField] public Vector2 VerticalBoundaries { get; private set; } = new Vector2(-10, 10);
        
        [field: SerializeField] public float Acceleration { get; private set; } = 10;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 180;
    }
}