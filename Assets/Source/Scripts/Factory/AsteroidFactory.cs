using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts
{
    public class AsteroidFactory
    {
        private readonly MovementConfig _movementConfig;
        private EntityView _prefab;

        public AsteroidFactory(MovementConfig movementConfig, EntityView prefab)
        {
            _movementConfig = movementConfig;
            _prefab = prefab;
        }

        public Entity Create(MovementData movementData, float size)
        {
            //TODO add view, collision and ignoring certain entity types

            var entityView = GameObject.Instantiate(_prefab);
            var entity = new Entity(EntityType.Asteroid);

            var movementComponent = new MovementComponent(_movementConfig, movementData);
            var damageComponent = new DamageComponent(new List<EntityType>() { EntityType.Asteroid, EntityType.Enemy, EntityType.Bullet, }, null);
            
            entity.FixedUpdatableComponents.Add(movementComponent);
            return entity;
        }
        
    }
}