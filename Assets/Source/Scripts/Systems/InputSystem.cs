using Source.Scripts.Input;

namespace Source.Scripts.Systems
{
    public class InputSystem : BaseSystem
    {
        private InputState _inputState;
        private PlayerInput _input;

        public InputSystem(InputState inputState, PlayerInput input)
        {
            _inputState = inputState;
            _input = input;
        }

        public override void OnUpdate(float deltaTime)
        {
            _inputState.AccelerationInput = _input.Ship.Accelerate.ReadValue<float>();
            _inputState.RotationInput = _input.Ship.Rotate.ReadValue<float>();
            _inputState.ShootInput = _input.Ship.Shoot.IsPressed();
            _inputState.SpecialShootInput = _input.Ship.ShootSpecial.IsPressed();
        }

        public override void OnFixedUpdate(float deltaTime)
        {

        }
    }
}