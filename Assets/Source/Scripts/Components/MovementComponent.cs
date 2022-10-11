using Source.Scripts.Configs;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class MovementComponent : UpdatableComponent
    {
        protected readonly MovementConfig _movementConfig;
        
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }
        public float Rotation { get; protected set; }
        
        public Vector2 Forward => new Vector2(Mathf.Cos(Rotation * Mathf.Deg2Rad), Mathf.Sin(Rotation * Mathf.Deg2Rad));

        public MovementComponent(MovementConfig config, Vector2 position, Vector2 velocity, float rotation)
        {
            _movementConfig = config;
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
        }
        
        public override void OnUpdate(float deltaTime)
        {
            UpdatePosition(deltaTime);
        }

        protected void UpdatePosition(float deltaTime)
        {
            Position += Velocity * deltaTime;
        }

    }
}