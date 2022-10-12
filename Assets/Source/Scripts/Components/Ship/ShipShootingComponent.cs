using Source.Scripts.Configs;
using Source.Scripts.Input;

namespace Source.Scripts.Components
{
    public class ShipShootingComponent : UpdatableComponent
    {
        private readonly ShootingConfig _shootingConfig;
        private readonly MovementData _movementData;
        private readonly InputState _inputState;
        private readonly BulletFactory _bulletFactory;

        private float _timer;

        public ShipShootingComponent(ShootingConfig shootingConfig, BulletFactory bulletFactory, InputState inputState, ref MovementData movementData)
        {
            _shootingConfig = shootingConfig;
            _movementData = movementData;
            _inputState = inputState;
            _bulletFactory = bulletFactory;
            
            _timer = 0;
        }

        public override void OnUpdate(float deltaTime)
        {
            TryToShoot(deltaTime);
        }

        private void TryToShoot(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer < _shootingConfig.BulletShotCooldown || !_inputState.ShootInput) return;

            ShootBullet();
            _timer = 0;
        }
        private void ShootBullet()
        {
            _bulletFactory.Create(_shootingConfig.BulletLifetime, _movementData);
        }
    }
}