using Source.Scripts.Configs;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class MovementComponent : UpdatableComponent
    {
        protected readonly MovementConfig _movementConfig;

        public MovementData MovementData;

        public MovementComponent(MovementConfig config, Vector2 position, Vector2 velocity, float rotation)
        {
            _movementConfig = config;
            MovementData = new MovementData
            {
                Position = position,
                Velocity = velocity,
                Rotation = rotation
            };
        }

        public MovementComponent(MovementConfig config, MovementData movementData)
        {
            _movementConfig = config;
            MovementData = movementData;
        }

        public override void OnUpdate(float deltaTime)
        {
            UpdatePosition(deltaTime);
        }

        protected void UpdatePosition(float deltaTime)
        {
            MovementData.Position += MovementData.Velocity * deltaTime;
        }

    }
}