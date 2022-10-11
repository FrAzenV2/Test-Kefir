using System;
using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Systems;

namespace Source.Scripts.Entities
{
    public abstract class BaseEntity
    {
        protected List<UpdatableComponent> _updatableComponents = new();
        protected List<UpdatableComponent> _fixedUpdatableComponents = new();

        public event Action<BaseEntity> OnErase;

        protected BaseEntity()
        {
            EntitiesUpdateSystem.Instance.AddEntity(this);
        }

        public void Erase()
        {
            OnErase?.Invoke(this);
        }

        public virtual void OnUpdate(float deltaTime)
        {
            foreach (var updatableComponent in _updatableComponents) updatableComponent.OnUpdate(deltaTime);
        }

        public virtual void OnFixedUpdate(float deltaTime)
        {
            foreach (var fixedUpdatableComponent in _fixedUpdatableComponents) fixedUpdatableComponent.OnUpdate(deltaTime);
        }
    }
}