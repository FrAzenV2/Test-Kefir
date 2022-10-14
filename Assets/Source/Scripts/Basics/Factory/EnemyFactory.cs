using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Components.Bullet;
using Source.Scripts.Components.Enemies;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Data;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Factory
{
    public class EnemyFactory
    {
        private readonly IEntityUpdater _entityUpdater;
        private readonly MovementConfig _movementConfig;
        private readonly MovementData _targetMovementData;
        private EntityView _prefab;

        public EnemyFactory(IEntityUpdater entityUpdater, Entity targetEntity, MovementConfig movementConfig, EntityView prefab)
        {
            _entityUpdater = entityUpdater;
            _movementConfig = movementConfig;
            _prefab = prefab;

            _targetMovementData = ((MovementComponent)targetEntity.FixedUpdatableComponents.Find(x => x is MovementComponent)).MovementData;
        }

        public Entity Create()
        {
            var entityView = Object.Instantiate(_prefab);
            var entity = new Entity(EntityType.Enemy, entityView);
            entityView.SetEntity(entity);

            var movementComponent = new EnemyMovementComponent(_targetMovementData, _movementConfig, Random.insideUnitCircle.normalized * 10, Vector2.one * Random.Range(_movementConfig.MinVelocity, _movementConfig.MaxVelocity), 0, entityView.transform);
            var damageComponent = new DamageComponent(new List<EntityType>() { EntityType.Enemy, EntityType.Asteroid }, ref entityView.OnEntityCollision, null);

            entity.FixedUpdatableComponents.Add(movementComponent);
            _entityUpdater.AddEntity(entity);

            return entity;
        }
    }
}