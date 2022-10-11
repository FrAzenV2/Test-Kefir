using Source.Scripts.Configs;
using UnityEngine;

namespace Source.Scripts.Components.Bullet
{
    public class BulletMovementComponent : MovementComponent
    {
        private const float BULLET_OFFSET = 0.4f;
        public BulletMovementComponent(MovementConfig config, MovementData movementData) : base(config, movementData)
        {
            MovementData.Position = movementData.Forward * BULLET_OFFSET;
            MovementData.Velocity = movementData.Forward * config.Acceleration;
        }
    }
}