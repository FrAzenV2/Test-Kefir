using System.Collections.Generic;
using Source.Scripts.Components;

namespace Source.Scripts.Entities
{
    public abstract class BaseEntity
    {
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);

        protected List<UpdatableComponent> _updatableComponents = new List<UpdatableComponent>();
        protected List<UpdatableComponent> _fixedUpdatableComponents = new List<UpdatableComponent>();
    }
}