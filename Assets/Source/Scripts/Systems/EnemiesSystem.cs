﻿using System.Collections.Generic;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Factory;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class EnemiesSystem : BaseSystem
    {
        private readonly EnemiesSystemConfig _config;
        private readonly EnemyFactory _enemyFactory;

        private readonly List<Entity> _activeEnemies = new ();
        private float _cooldownTimer;

        public EnemiesSystem(EnemiesSystemConfig config, EnemyFactory enemyFactory)
        {
            _config = config;
            _enemyFactory = enemyFactory;

            _cooldownTimer = 0;
        }

        public override void OnUpdate(float deltaTime)
        {
            TryToSpawnEnemies(deltaTime);
        }

        private void TryToSpawnEnemies(float deltaTime)
        {
            if(_activeEnemies.Count>0) return;

            _cooldownTimer += deltaTime;

            if (_cooldownTimer < _config.SpawnCooldown) return;
            
            SpawnEnemies();
            _cooldownTimer = 0;

        }
        private void SpawnEnemies()
        {
            var amount = Random.Range(_config.MinEnemiesPerSpawn, _config.MaxEnemiesPerSpawn + 1);

            for (var i = 0; i < amount; i++)
            {
                var entity = _enemyFactory.Create();
                _activeEnemies.Add(entity);
                entity.OnErase += EraseEnemyFromActiveEntities;
            }
        }
        
        private void EraseEnemyFromActiveEntities(Entity entity)
        {
            entity.OnErase -= EraseEnemyFromActiveEntities;
            _activeEnemies.Remove(entity);
        }

        public override void OnFixedUpdate(float deltaTime) { }
    }
}