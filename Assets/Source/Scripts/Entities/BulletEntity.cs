using Source.Scripts.Components;
using Source.Scripts.Components.Bullet;
using Source.Scripts.Configs;

namespace Source.Scripts.Entities
{
    public class BulletEntity : BaseEntity
    {
        private MovementComponent _movementComponent;
        private DieOverTimeComponent _dieOverTimeComponent;

        public BulletEntity(MovementConfig movementConfig, MovementData baseMovementData, float lifetime)
        {
            _movementComponent = new BulletMovementComponent(movementConfig, baseMovementData);
            _dieOverTimeComponent = new DieOverTimeComponent(lifetime);

            _dieOverTimeComponent.Died += Erase;

            _fixedUpdatableComponents.Add(_movementComponent);
            _updatableComponents.Add(_dieOverTimeComponent);
        }

    }
}