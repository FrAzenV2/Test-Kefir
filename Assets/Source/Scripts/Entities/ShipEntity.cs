using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Configs;
using Source.Scripts.Input;
using UnityEngine;

namespace Source.Scripts.Entities
{
    public class ShipEntity : BaseEntity
    {
        public ShipMovementComponent MovementComponent { get; private set; }
        
        public ShipEntity(MovementConfig movementConfig, InputState inputState)
        {
            MovementComponent = new ShipMovementComponent(inputState, movementConfig, Vector2.zero, Vector2.zero, 0);
            
            _fixedUpdatableComponents.Add(MovementComponent);
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var updatableComponent in _updatableComponents)
            {
                updatableComponent.OnUpdate(deltaTime);
            }
        }
        
        public override void OnFixedUpdate(float deltaTime)
        {
            foreach (var fixedUpdatableComponent in _fixedUpdatableComponents)
            {
                fixedUpdatableComponent.OnUpdate(deltaTime);
            }
        }

    }
}