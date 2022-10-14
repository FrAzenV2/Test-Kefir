using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Components.Bullet;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Data;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Factory
{
    public class LaserFactory
    {
        private readonly IEntityUpdater _entityUpdater;
        private readonly MovementConfig _movementConfig;
        private EntityView _prefab;

        public LaserFactory(IEntityUpdater entityUpdater, MovementConfig movementConfig, EntityView prefab)
        {
            _entityUpdater = entityUpdater;
            _movementConfig = movementConfig;
            _prefab = prefab;
        }

        public Entity Create(float lifetime, MovementData movementData, List<EntityType> ignoreTypes)
        {
            //TODO add view, collision and ignoring certain entity types

            var entityView = Object.Instantiate(_prefab);
            var entity = new Entity(EntityType.Laser, entityView);
            entityView.SetEntity(entity);

            var movementComponent = new BulletMovementComponent(_movementConfig, movementData, entityView.transform);
            var dieOverTimeComponent = new DieOverTimeComponent(lifetime, entity.Erase);
            var damageComponent = new DamageComponent(ignoreTypes, ref entityView.OnEntityCollision);


            entity.FixedUpdatableComponents.Add(movementComponent);
            entity.UpdatableComponents.Add(dieOverTimeComponent);

            _entityUpdater.AddEntity(entity);

            return entity;
        }
    }
}