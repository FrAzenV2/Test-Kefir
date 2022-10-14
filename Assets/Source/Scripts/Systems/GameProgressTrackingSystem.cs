using Source.Scripts.Components;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Data;
using Source.Scripts.Entities;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class GameProgressTrackingSystem : BaseSystem
    {
        private readonly MovementData _playerMovementData;
        private readonly ShipShootingComponent _playerShootingComponent;
        private readonly IEntityUpdater _entityUpdater;
        private readonly MonoBehaviour _systemsUpdater;
        private readonly PlayerDataView _playerDataView;
        
        private float _inGameTime;
        private float _totalScore;

        public GameProgressTrackingSystem(Entity playerEntity, IEntityUpdater entityUpdater, MonoBehaviour systemsUpdater, PlayerDataView playerDataView)
        {
            _playerMovementData = ((MovementComponent)playerEntity.FixedUpdatableComponents.Find(x => x is MovementComponent)).MovementData;
            _playerShootingComponent = (ShipShootingComponent)playerEntity.UpdatableComponents.Find(x => x is ShipShootingComponent);

            _playerDataView = playerDataView;
            _entityUpdater = entityUpdater;
            _systemsUpdater = systemsUpdater;

            _totalScore = 0;
            _inGameTime = 0;
            
            _playerDataView.UpdateTotalScore((int)_totalScore);
            
            playerEntity.OnErase += _ => OnPlayerDied();
        }

        public void AddScore(float amount)
        {
            _totalScore += amount;
            _playerDataView.UpdateTotalScore((int)_totalScore);
        }
        
        private void OnPlayerDied()
        {
            _entityUpdater.RemoveAllEntities();
            _playerDataView.ShowDeathScreen();
            _systemsUpdater.gameObject.SetActive(false);
        }


        public override void OnUpdate(float deltaTime)
        {
            _inGameTime += deltaTime;
            _playerDataView.UpdateVelocity(_playerMovementData.Velocity);
            _playerDataView.UpdatePositionText(_playerMovementData.Position);
            _playerDataView.UpdateLasers(_playerShootingComponent.CurrentLasers, _playerShootingComponent.LasersCooldownTimer);
            _playerDataView.UpdateTotalTime((int)_inGameTime);
            _playerDataView.UpdateRotationText(_playerMovementData.Rotation);
        }
        
        
        public override void OnFixedUpdate(float deltaTime){}
    }
}