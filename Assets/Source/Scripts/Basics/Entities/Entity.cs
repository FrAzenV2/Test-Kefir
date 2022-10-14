using System;
using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.Components.UnityComponents;
using Source.Scripts.Enums;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Entities
{
    public class Entity
    {
        public EntityType EntityType;

        public List<UpdatableComponent> UpdatableComponents = new();
        public List<UpdatableComponent> FixedUpdatableComponents = new();

        public event Action<Entity> OnErase;

        private EntityView _entityView;

        public Entity(EntityType entityType, EntityView entityView)
        {
            EntityType = entityType;
            _entityView = entityView;
        }

        public void Erase()
        {
            Object.Destroy(_entityView.gameObject);
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