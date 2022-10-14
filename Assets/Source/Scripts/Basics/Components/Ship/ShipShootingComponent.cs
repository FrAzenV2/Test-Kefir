using System.Collections.Generic;
using Source.Scripts.Configs;
using Source.Scripts.Data;
using Source.Scripts.Enums;
using Source.Scripts.Factory;
using Source.Scripts.Input;

namespace Source.Scripts.Components
{
    public class ShipShootingComponent : UpdatableComponent
    {
        private readonly ShootingConfig _shootingConfig;
        private readonly MovementData _movementData;
        private readonly InputState _inputState;
        private readonly BulletFactory _bulletFactory;
        private readonly LaserFactory _laserFactory;

        private int _currentLasers;
        private float _lasersCooldown;
        private float _timer;

        public float LasersCooldownTimer => _shootingConfig.LaserCooldown - _lasersCooldown;
        public int CurrentLasers => _currentLasers;

        public ShipShootingComponent(ShootingConfig shootingConfig, BulletFactory bulletFactory, LaserFactory laserFactory, InputState inputState, MovementData movementData)
        {
            _shootingConfig = shootingConfig;
            _movementData = movementData;
            _inputState = inputState;
            _laserFactory = laserFactory;
            _bulletFactory = bulletFactory;

            _currentLasers = shootingConfig.MaxLasers;
            _timer = 0;
            _lasersCooldown = 0;
        }

        public override void OnUpdate(float deltaTime)
        {
            TryToGiveLaser(deltaTime);
            TryToShoot(deltaTime);
        }

        private void TryToGiveLaser(float deltaTime)
        {
            if (_currentLasers >= _shootingConfig.MaxLasers) return;
            _lasersCooldown += deltaTime;

            if (_lasersCooldown < _shootingConfig.LaserCooldown) return;
            _lasersCooldown = 0;
            _currentLasers++;
        }

        private void TryToShoot(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer < _shootingConfig.BulletShotCooldown || !(_inputState.ShootInput || _inputState.SpecialShootInput && _currentLasers > 0)) return;

            if (_inputState.SpecialShootInput && _currentLasers > 0)
                ShootLaser();
            else if (_inputState.ShootInput)
                ShootBullet();
            _timer = 0;
        }
        private void ShootLaser()
        {
            _currentLasers--;
            _laserFactory.Create(_shootingConfig.LaserLifetime, _movementData, new List<EntityType>() { EntityType.Player, EntityType.Laser });
        }
        private void ShootBullet()
        {
            _bulletFactory.Create(_shootingConfig.BulletLifetime, _movementData, new List<EntityType>() { EntityType.Player, EntityType.Bullet, EntityType.Laser });
        }
    }
}