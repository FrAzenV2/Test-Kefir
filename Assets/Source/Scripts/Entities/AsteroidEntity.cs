using Source.Scripts.Components;
using Source.Scripts.Configs;


namespace Source.Scripts.Entities
{
    public class AsteroidEntity : BaseEntity
    {
        private MovementComponent _movementComponent;
        private int _size;

        public AsteroidEntity(MovementConfig movementConfig, MovementData movementData, int size)
        {
            _movementComponent = new MovementComponent(movementConfig, movementData);
            _size = size;

            _fixedUpdatableComponents.Add(_movementComponent);
        }

    }
}