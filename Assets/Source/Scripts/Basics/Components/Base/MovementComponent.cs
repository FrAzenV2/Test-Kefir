using Source.Scripts.Configs;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class MovementComponent : UpdatableComponent
    {
        protected readonly MovementConfig _movementConfig;

        public MovementData MovementData;

        //For view update
        private Transform _viewTransform;

        public MovementComponent(MovementConfig config, Vector2 position, Vector2 velocity, float rotation, Transform viewTransform)
        {
            _movementConfig = config;
            MovementData = new MovementData
            {
                Position = position,
                Velocity = velocity,
                Rotation = rotation
            };
            _viewTransform = viewTransform;

            UpdatePosition(0);
        }

        public MovementComponent(MovementConfig config, MovementData movementData, Transform viewTransform)
        {
            _movementConfig = config;
            _viewTransform = viewTransform;
            MovementData = new MovementData(movementData);

            UpdatePosition(0);
            UpdateRotation(0);
        }

        public override void OnUpdate(float deltaTime)
        {
            UpdatePosition(deltaTime);
        }

        private void UpdatePosition(float deltaTime)
        {
            MovementData.Position += MovementData.Velocity * deltaTime;

            if (MovementData.Position.x > _movementConfig.HorizontalBoundaries.y) MovementData.Position.x = _movementConfig.HorizontalBoundaries.x;
            if (MovementData.Position.x < _movementConfig.HorizontalBoundaries.x) MovementData.Position.x = _movementConfig.HorizontalBoundaries.y;
            if (MovementData.Position.y > _movementConfig.VerticalBoundaries.y) MovementData.Position.y = _movementConfig.VerticalBoundaries.x;
            if (MovementData.Position.y < _movementConfig.VerticalBoundaries.x) MovementData.Position.y = _movementConfig.VerticalBoundaries.y;

            _viewTransform.position = MovementData.Position;
        }

        protected virtual void UpdateRotation(float deltaTime)
        {
            _viewTransform.eulerAngles = Vector3.forward * MovementData.Rotation;
        }

        protected virtual void UpdateVelocity(float deltaTime)
        {
            MovementData.Velocity = MovementData.Velocity.normalized * (MovementData.Velocity.magnitude - _movementConfig.Deceleration * deltaTime);
            if (MovementData.Velocity.magnitude > _movementConfig.MaxVelocity) MovementData.Velocity = MovementData.Velocity.normalized * _movementConfig.MaxVelocity;
            if (MovementData.Velocity.magnitude < _movementConfig.MinVelocity) MovementData.Velocity = MovementData.Velocity.normalized * _movementConfig.MinVelocity;
        }


    }
}