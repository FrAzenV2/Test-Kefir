using Source.Scripts.Configs;
using Source.Scripts.Input;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class ShipMovementComponent : MovementComponent
    {
        private readonly InputState _inputState;
        
        public ShipMovementComponent(InputState inputState,MovementConfig config, Vector2 position, Vector2 velocity, float rotation) : base(config, position, velocity, rotation)
        {
            _inputState = inputState;
        }

        public override void OnUpdate(float deltaTime)
        {
            UpdateRotation(deltaTime);
            UpdateVelocity(deltaTime);
            base.OnUpdate(deltaTime);
        }
        
        private void UpdateRotation(float deltaTime)
        {
            Rotation += _inputState.RotationInput * _movementConfig.RotationSpeed * deltaTime;
        }

        private void UpdateVelocity(float deltaTime)
        {
            Velocity += Forward * _inputState.AccelerationInput * _movementConfig.Acceleration * deltaTime;
        }
        
        
    }
}