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
            //TODO HOW TO UPDATE VIEW???
            MovementData.Position += MovementData.Velocity * deltaTime;

            if (MovementData.Position.x > _movementConfig.HorizontalBoundaries.y)
            {
                MovementData.Position.x = _movementConfig.HorizontalBoundaries.x;
            }
            if (MovementData.Position.x < _movementConfig.HorizontalBoundaries.x)
            {
                MovementData.Position.x = _movementConfig.HorizontalBoundaries.y;
            }
            if (MovementData.Position.y > _movementConfig.VerticalBoundaries.y)
            {
                MovementData.Position.y = _movementConfig.HorizontalBoundaries.x;
            }
            if (MovementData.Position.y < _movementConfig.VerticalBoundaries.x)
            {
                MovementData.Position.y = _movementConfig.HorizontalBoundaries.y;
            }
            
        }

    }
}