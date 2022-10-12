using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Components.Bullet;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts
{
    public class BulletFactory
    {
        private readonly MovementConfig _movementConfig;
        private EntityView _prefab;
        
        public BulletFactory(MovementConfig movementConfig, EntityView prefab)
        {
            _movementConfig = movementConfig;
            _prefab = prefab;
        }

        public Entity Create(float lifetime, MovementData movementData)
        {
            //TODO add view, collision and ignoring certain entity types
            
            var entityView = GameObject.Instantiate(_prefab);
            var entity = new Entity(EntityType.Bullet);
            
            var movementComponent = new BulletMovementComponent(_movementConfig, movementData);
            var dieOverTimeComponent = new DieOverTimeComponent(lifetime, entity.Erase);
            var damageComponent = new DamageComponent(new List<EntityType>() { EntityType.Player, EntityType.Bullet, }, null);
            
            
            entity.FixedUpdatableComponents.Add(movementComponent);
            entity.UpdatableComponents.Add(dieOverTimeComponent);

            return entity;
        }
       
    }
}