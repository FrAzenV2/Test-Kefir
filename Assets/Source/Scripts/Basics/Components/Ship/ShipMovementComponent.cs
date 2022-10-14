using Source.Scripts.Configs;
using Source.Scripts.Input;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class ShipMovementComponent : MovementComponent
    {
        private readonly InputState _inputState;

        public ShipMovementComponent(InputState inputState, MovementConfig config, Vector2 position, Vector2 velocity, float rotation, Transform viewTransform) : base(config, position, velocity, rotation, viewTransform)
        {
            _inputState = inputState;
        }

        public override void OnUpdate(float deltaTime)
        {
            UpdateRotation(deltaTime);
            UpdateVelocity(deltaTime);
            base.OnUpdate(deltaTime);
        }

        protected override void UpdateRotation(float deltaTime)
        {
            MovementData.Rotation += _inputState.RotationInput * _movementConfig.RotationSpeed * deltaTime;
            base.UpdateRotation(deltaTime);
        }

        protected virtual void UpdateVelocity(float deltaTime)
        {
            if (_inputState.AccelerationInput)
                MovementData.Velocity += MovementData.Forward * _movementConfig.Acceleration * deltaTime;
            base.UpdateVelocity(deltaTime);
        }


    }
}