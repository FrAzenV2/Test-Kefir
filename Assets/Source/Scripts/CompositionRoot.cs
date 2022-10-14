﻿using System;
using System.Collections.Generic;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using Source.Scripts.Factory;
using Source.Scripts.Input;
using Source.Scripts.Systems;
using UnityEngine;

namespace Source.Scripts
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private List<EntityData> _entityDatas = new();

        public static CompositionRoot Instance;

        private List<Entity> _entities = new();
        private List<BaseSystem> _baseSystems = new();
        private HashSet<Entity> _entitiesToRemove = new();
        private HashSet<Entity> _entitiesToAdd = new();

        private void Awake()
        {
            if (Instance) Destroy(this);
            else Instance = this;

            //TODO add systems and factories init here
            //TODO add configs and prefabs here

            var inputState = new InputState();
            var playerInput = new PlayerInput();
            playerInput.Enable();

            var inputSystem = new InputSystem(inputState, playerInput);
            _baseSystems.Add(inputSystem);

            var asteroidsData = _entityDatas.Find(x => x.EntityType == EntityType.Asteroid);
            var asteroidsSystem = new AsteroidsSystem((AsteroidsConfig)asteroidsData.Configs.Find(x => x is AsteroidsConfig),
                (MovementConfig)asteroidsData.Configs.Find(x => x is MovementConfig), asteroidsData.Prefab);
            _baseSystems.Add(asteroidsSystem);

            var bulletsData = _entityDatas.Find(x => x.EntityType == EntityType.Bullet);
            var bulletFactory = new BulletFactory((MovementConfig)bulletsData.Configs.Find(x => x is MovementConfig), bulletsData.Prefab);

            var lasersData = _entityDatas.Find(x => x.EntityType == EntityType.Laser);
            var lasersFactory = new LaserFactory((MovementConfig)lasersData.Configs.Find(x => x is MovementConfig), lasersData.Prefab);

            var shipData = _entityDatas.Find(x => x.EntityType == EntityType.Player);
            var shipFactory = new ShipFactory((MovementConfig)shipData.Configs.Find(x => x is MovementConfig),
                (ShootingConfig)shipData.Configs.Find(x => x is ShootingConfig), bulletFactory, lasersFactory, inputState, shipData.Prefab);

            var ship = shipFactory.Create();

            var enemyData = _entityDatas.Find(x => x.EntityType == EntityType.Enemy);
            var enemyFactory = new EnemyFactory(ship, (MovementConfig)enemyData.Configs.Find(x => x is MovementConfig), enemyData.Prefab);
            var enemiesSystem = new EnemiesSystem((EnemiesSystemConfig)enemyData.Configs.Find(x => x is EnemiesSystemConfig), enemyFactory);
            
            _baseSystems.Add(enemiesSystem);

            //TODO add ship ui representation and game progress tracking

        }

        private void Update()
        {
            foreach (var baseSystem in _baseSystems) baseSystem.OnUpdate(Time.deltaTime);
            
            foreach (var entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }
            _entitiesToAdd.Clear();
            
            foreach (var entity in _entities) entity.OnUpdate(Time.deltaTime);
            
            foreach (var entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }
            _entitiesToRemove.Clear();
        }

        private void FixedUpdate()
        {
            foreach (var baseSystem in _baseSystems) baseSystem.OnFixedUpdate(Time.fixedDeltaTime);
            foreach (var entity in _entities) entity.OnFixedUpdate(Time.fixedDeltaTime);
        }

        public void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
            entity.OnErase += RemoveEntity;
        }

        public void ClearAllEntities()
        {
            foreach (var entity in _entities)
            {
                _entitiesToRemove.Add(entity);
            }
        }

        private void RemoveEntity(Entity entity)
        {
            entity.OnErase -= RemoveEntity;
            _entitiesToRemove.Add(entity);
        }

    }

    [Serializable]
    public class EntityData
    {
        public List<ScriptableObject> Configs = new();
        public EntityView Prefab;
        public EntityType EntityType;
    }
}