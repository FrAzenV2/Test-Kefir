using System;
using System.Collections.Generic;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Configs;
using Source.Scripts.Entities;
using Source.Scripts.Enums;
using Source.Scripts.Factory;
using Source.Scripts.Input;
using Source.Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private PlayerDataView _playerDataView;
        [SerializeField] private List<EntityData> _entityDatas = new();
        
        private List<BaseSystem> _baseSystems = new();
        
        private void Awake()
        {
            var entityUpdateSystem = new EntityUpdateSystem();
            _baseSystems.Add(entityUpdateSystem);
            
            //INPUT
            var inputState = new InputState();
            var playerInput = new PlayerInput();
            playerInput.Enable();

            var inputSystem = new InputSystem(inputState, playerInput);
            _baseSystems.Add(inputSystem);

            //BULLETS
            var bulletsData = _entityDatas.Find(x => x.EntityType == EntityType.Bullet);
            var bulletFactory = new BulletFactory(entityUpdateSystem,(MovementConfig)bulletsData.Configs.Find(x => x is MovementConfig), bulletsData.Prefab);

            //LASERS
            var lasersData = _entityDatas.Find(x => x.EntityType == EntityType.Laser);
            var lasersFactory = new LaserFactory(entityUpdateSystem,(MovementConfig)lasersData.Configs.Find(x => x is MovementConfig), lasersData.Prefab);

            //PLAYER SHIP
            var shipData = _entityDatas.Find(x => x.EntityType == EntityType.Player);
            var shipFactory = new ShipFactory(entityUpdateSystem,(MovementConfig)shipData.Configs.Find(x => x is MovementConfig),
                (ShootingConfig)shipData.Configs.Find(x => x is ShootingConfig), bulletFactory, lasersFactory, inputState, shipData.Prefab);

            var ship = shipFactory.Create();

            //GAME PROGRESS TRACKING
            var gameProgressTracking = new GameProgressTrackingSystem(ship, entityUpdateSystem, this,_playerDataView);
            _baseSystems.Add(gameProgressTracking);
            
            //ASTEROIDS
            var asteroidsData = _entityDatas.Find(x => x.EntityType == EntityType.Asteroid);
            var asteroidsFactory = new AsteroidFactory(entityUpdateSystem, (MovementConfig)asteroidsData.Configs.Find(x => x is MovementConfig), asteroidsData.Prefab);
            var asteroidsSystem = new AsteroidsSystem((AsteroidsConfig)asteroidsData.Configs.Find(x => x is AsteroidsConfig), asteroidsFactory, gameProgressTracking.AddScore);
            _baseSystems.Add(asteroidsSystem);

            //ENEMIES
            var enemyData = _entityDatas.Find(x => x.EntityType == EntityType.Enemy);
            var enemyFactory = new EnemyFactory(entityUpdateSystem, ship, (MovementConfig)enemyData.Configs.Find(x => x is MovementConfig), enemyData.Prefab);
            var enemiesSystem = new EnemiesSystem((EnemiesSystemConfig)enemyData.Configs.Find(x => x is EnemiesSystemConfig), enemyFactory, gameProgressTracking.AddScore);

            _baseSystems.Add(enemiesSystem);
        }

        private void Update()
        {
            foreach (var baseSystem in _baseSystems) baseSystem.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            foreach (var baseSystem in _baseSystems) baseSystem.OnFixedUpdate(Time.fixedDeltaTime);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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