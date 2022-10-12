using System.Collections.Generic;
using Source.Scripts.Entities;
using UnityEngine;

namespace Source.Scripts
{
    public class CompositionRoot : MonoBehaviour
    {
        public static CompositionRoot Instance;

        private List<Entity> _entities = new();

        private void Awake()
        {
            if (Instance) Destroy(this);
            else Instance = this;
            
            //TODO add systems and factories init here
            //TODO add configs and prefabs here
        }

        private void Update()
        {
            foreach (var entity in _entities) entity.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            foreach (var entity in _entities) entity.OnFixedUpdate(Time.fixedDeltaTime);
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
            entity.OnErase += RemoveEntity;
        }

        public void ClearAllEntities()
        {
            _entities.Clear();
        }

        private void RemoveEntity(Entity entity)
        {
            entity.OnErase -= RemoveEntity;
            _entities.Remove(entity);
        }




    }
}