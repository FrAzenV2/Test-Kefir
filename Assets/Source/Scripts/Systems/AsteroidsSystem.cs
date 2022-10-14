using System;
using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Systems
{
    public class AsteroidsSystem : BaseSystem
    {
        private readonly AsteroidsConfig _asteroidsConfig;
        private readonly AsteroidFactory _asteroidFactory;
        private Action<float> _rewardCallback;

        private Dictionary<Entity, float> _aliveAsteroids = new();

        private float _timer;

        public AsteroidsSystem(AsteroidsConfig asteroidsConfig, AsteroidFactory asteroidFactory, Action<float> rewardCallback)
        {
            _asteroidsConfig = asteroidsConfig;
            _asteroidFactory = asteroidFactory;
            _rewardCallback = rewardCallback;
            _timer = 0;
        }

        public override void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer < _asteroidsConfig.SpawnTick) return;

            TrySpawnAsteroid();
        }

        private void TrySpawnAsteroid()
        {
            if (_aliveAsteroids.Count >= _asteroidsConfig.MaxAmount) return;
            _timer = 0;

            var size = Random.Range(_asteroidsConfig.MinSize, _asteroidsConfig.MaxSize);
            var asteroid = _asteroidFactory.Create(size);
            _aliveAsteroids.Add(asteroid, size);
            asteroid.OnErase += TryToSplit;
        }

        private void TryToSplit(Entity asteroid)
        {
            asteroid.OnErase -= TryToSplit;

            var previousSize = _aliveAsteroids[asteroid];

            CalculateReward(previousSize);
            _aliveAsteroids.Remove(asteroid);

            var newSize = previousSize / 2;

            if (newSize < _asteroidsConfig.MinSize) return;

            var previousMovementData = ((MovementComponent)asteroid.FixedUpdatableComponents.Find(x => x is MovementComponent)).MovementData;


            //TODO think about moving out split parameter out. Maybe randomize it?
            for (var i = 0; i < 2; i++)
            {
                previousMovementData.Rotation = Random.Range(0, 360);
                previousMovementData.Velocity = previousMovementData.Forward * previousMovementData.Velocity.magnitude * _asteroidsConfig.SplitAsteroidsVelocityMultiplier;

                var newAsteroid = _asteroidFactory.Create(previousMovementData, newSize);
                _aliveAsteroids.Add(newAsteroid, newSize);
                newAsteroid.OnErase += TryToSplit;
            }
        }
        private void CalculateReward(float previousSize)
        {
            var t = Mathf.InverseLerp(_asteroidsConfig.MinSize, _asteroidsConfig.MaxSize, previousSize);
            var rewardAmount = Mathf.Lerp(_asteroidsConfig.MinReward, _asteroidsConfig.MaxReward, 1 - t);
            _rewardCallback?.Invoke(rewardAmount);
        }
        public override void OnFixedUpdate(float deltaTime) { }
    }
}