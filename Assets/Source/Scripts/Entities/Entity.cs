using System;
using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Enums;

namespace Source.Scripts.Entities
{
    public class Entity
    {
        public EntityType EntityType;
        
        public List<UpdatableComponent> UpdatableComponents = new();
        public List<UpdatableComponent> FixedUpdatableComponents = new();

        public event Action<Entity> OnErase;

        public Entity(EntityType entityType)
        {
            EntityType = entityType;
            CompositionRoot.Instance.AddEntity(this);
        }

        public void Erase()
        {
            OnErase?.Invoke(this);
        }

        public virtual void OnUpdate(float deltaTime)
        {
            foreach (var updatableComponent in UpdatableComponents) updatableComponent.OnUpdate(deltaTime);
        }

        public virtual void OnFixedUpdate(float deltaTime)
        {
            foreach (var fixedUpdatableComponent in FixedUpdatableComponents) fixedUpdatableComponent.OnUpdate(deltaTime);
        }
    }
}