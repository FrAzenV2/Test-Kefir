using System.Collections.Generic;
using Source.Scripts.Entities;

namespace Source.Scripts.Systems
{
    public class EntityUpdateSystem : BaseSystem, IEntityUpdater
    {
        private readonly List<Entity> _entities = new();
        private readonly HashSet<Entity> _entitiesToRemove = new();
        private readonly HashSet<Entity> _entitiesToAdd = new();

        public void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
            entity.OnErase += RemoveEntity;
        }

        public void RemoveAllEntities()
        {
            foreach (var entity in _entities) _entitiesToRemove.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            entity.OnErase -= RemoveEntity;
            _entitiesToRemove.Add(entity);
        }
        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entitiesToAdd) _entities.Add(entity);
            _entitiesToAdd.Clear();

            foreach (var entity in _entities) entity.OnUpdate(deltaTime);

            foreach (var entity in _entitiesToRemove) _entities.Remove(entity);
            _entitiesToRemove.Clear();
        }
        public override void OnFixedUpdate(float deltaTime)
        {
            foreach (var entity in _entities) entity.OnFixedUpdate(deltaTime);
        }
    }
}