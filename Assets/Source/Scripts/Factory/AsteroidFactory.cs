using System;
using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Data;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Scripts.Factory
{
    public class AsteroidFactory
    {
        private readonly IEntityUpdater _entityUpdater;
        private readonly MovementConfig _movementConfig;
        private EntityView _prefab;

        public AsteroidFactory(IEntityUpdater entityUpdater, MovementConfig movementConfig, EntityView prefab)
        {
            _entityUpdater = entityUpdater;
            _movementConfig = movementConfig;
            _prefab = prefab;
        }

        public Entity Create(float size)
        {
            var movementData = new MovementData()
            {
                Position = new Vector2(_movementConfig.HorizontalBoundaries.x - 100, _movementConfig.VerticalBoundaries.x - 100),
                Rotation = Random.value * 360,
                Velocity = Random.insideUnitCircle.normalized * Random.Range(_movementConfig.MinVelocity, _movementConfig.MaxVelocity)
            };
            return Create(movementData, size);
        }

        public Entity Create(MovementData movementData, float size)
        {
            //TODO add view, collision and ignoring certain entity types

            var entityView = Object.Instantiate(_prefab);
            entityView.transform.localScale = Vector3.one * size;

            var entity = new Entity(EntityType.Asteroid,entityView);
            entityView.SetEntity(entity);

            var movementComponent = new MovementComponent(_movementConfig, movementData, entityView.transform);
            var damageComponent = new DamageComponent(new List<EntityType>() { EntityType.Asteroid, EntityType.Enemy}, ref entityView.OnEntityCollision, entity.Erase);

            entity.FixedUpdatableComponents.Add(movementComponent);
            
            _entityUpdater.AddEntity(entity);
            
            return entity;
        }

    }
}