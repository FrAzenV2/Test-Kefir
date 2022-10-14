using Source.Scripts.Configs;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Components.Enemies
{
    public class EnemyMovementComponent : MovementComponent
    {
        private readonly MovementData _target;
        
        public EnemyMovementComponent(MovementData target,MovementConfig config, Vector2 position, Vector2 velocity, float rotation, Transform viewTransform) : base(config, position, velocity, rotation, viewTransform)
        {
            _target = target;
        }

        public override void OnUpdate(float deltaTime)
        {
            UpdateVelocity(deltaTime);    
            base.OnUpdate(deltaTime);
        }
        
        protected override void UpdateVelocity(float deltaTime)
        {
            var newDirection = (_target.Position - MovementData.Position).normalized;

            MovementData.Velocity = Vector2.MoveTowards(MovementData.Velocity.normalized, newDirection, _movementConfig.RotationSpeed * deltaTime) * MovementData.Velocity.magnitude;
            
            base.UpdateVelocity(deltaTime);
        }

    }
}