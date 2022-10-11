using Source.Scripts.Configs;
using Source.Scripts.Input;

namespace Source.Scripts.Components
{
    public class ShipShootingComponent : UpdatableComponent
    {
        private readonly ShootingConfig _shootingConfig;
        private readonly MovementData _movementData;
        private readonly InputState _inputState;

        private float _timer;

        public ShipShootingComponent(ShootingConfig shootingConfig, ref MovementData movementData, InputState inputState)
        {
            _shootingConfig = shootingConfig;
            _movementData = movementData;
            _inputState = inputState;

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
            //TODO add bullet spawning
        }
    }
}