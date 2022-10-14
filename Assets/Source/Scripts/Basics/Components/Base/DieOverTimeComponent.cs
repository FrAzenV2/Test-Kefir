using System;

namespace Source.Scripts.Components
{
    public class DieOverTimeComponent : UpdatableComponent
    {

        public event Action Died;

        private float _lifetime;

        public DieOverTimeComponent(float lifetime, Action OnDied)
        {
            _lifetime = lifetime;
            Died += OnDied;
        }

        public override void OnUpdate(float deltaTime)
        {
            _lifetime -= deltaTime;

            if (_lifetime > 0) return;

            Died?.Invoke();
        }
    }
}