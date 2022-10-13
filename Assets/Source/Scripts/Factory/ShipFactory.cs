using Source.Scripts.Components;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using Source.Scripts.Input;
using UnityEngine;

namespace Source.Scripts
{
    public class ShipFactory
    {
        private readonly MovementConfig _shipMovementConfig;
        private readonly ShootingConfig _shootingConfig;
        private readonly InputState _inputState;
        private readonly BulletFactory _bulletFactory;
        private EntityView _prefab;

        public ShipFactory(MovementConfig shipMovementConfig, ShootingConfig shipShootingConfig, BulletFactory bulletFactory, InputState inputState, EntityView prefab)
        {
            _shipMovementConfig = shipMovementConfig;
            _shootingConfig = shipShootingConfig;
            _bulletFactory = bulletFactory;
            _inputState = inputState;
            _prefab = prefab;
        }

        public Entity Create()
        {
            //TODO add view
            var entityView = Object.Instantiate(_prefab);
            var entity = new Entity(EntityType.Player,entityView);

            var movementComponent = new ShipMovementComponent(_inputState, _shipMovementConfig, Vector2.zero, Vector2.zero, 0, entityView.transform);
            var shootingComponent = new ShipShootingComponent(_shootingConfig, _bulletFactory, _inputState, movementComponent.MovementData);

            entity.FixedUpdatableComponents.Add(movementComponent);
            entity.UpdatableComponents.Add(shootingComponent);

            return entity;
        }


    }
}