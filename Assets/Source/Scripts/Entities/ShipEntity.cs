using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Configs;
using Source.Scripts.Input;
using UnityEngine;

namespace Source.Scripts.Entities
{
    public class ShipEntity : BaseEntity
    {
        private ShipMovementComponent _movementComponent;
        private ShipShootingComponent _shootingComponent;

        public ShipEntity(MovementConfig movementConfig, ShootingConfig shootingConfig, InputState inputState)
        {
            _movementComponent = new ShipMovementComponent(inputState, movementConfig, Vector2.zero, Vector2.zero, 0);
            _shootingComponent = new ShipShootingComponent(shootingConfig, ref _movementComponent.MovementData, inputState);

            _updatableComponents.Add(_shootingComponent);
            _fixedUpdatableComponents.Add(_movementComponent);
        }


    }
}