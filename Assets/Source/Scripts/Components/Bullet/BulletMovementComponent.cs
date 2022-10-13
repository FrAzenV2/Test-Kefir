using Source.Scripts.Configs;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Components.Bullet
{
    public class BulletMovementComponent : MovementComponent
    {
        private const float BULLET_OFFSET = 0.4f;
        public BulletMovementComponent(MovementConfig config, MovementData movementData, Transform viewTransform) : base(config, movementData, viewTransform)
        {
            MovementData.Position += movementData.Forward * BULLET_OFFSET;
            MovementData.Velocity = movementData.Forward * config.MaxVelocity;
        }
    }
}