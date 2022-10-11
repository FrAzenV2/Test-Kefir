using System;
using System.Collections.Generic;
using Source.Scripts.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class EntitiesUpdateSystem : MonoBehaviour
    {
        public static EntitiesUpdateSystem Instance;

        private List<BaseEntity> _entities = new();

        private void Awake()
        {
            if (Instance) Destroy(this);
            else Instance = this;
        }

        private void Update()
        {
            foreach (var entity in _entities) entity.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            foreach (var entity in _entities) entity.OnFixedUpdate(Time.fixedDeltaTime);
        }

        public void AddEntity(BaseEntity baseEntity)
        {
            _entities.Add(baseEntity);
            baseEntity.OnErase += RemoveEntity;
        }

        private void RemoveEntity(BaseEntity entity)
        {
            entity.OnErase -= RemoveEntity;
            _entities.Remove(entity);
        }


    }
}