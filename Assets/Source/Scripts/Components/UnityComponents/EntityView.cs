using System;
using Source.Scripts.Entities;
using UnityEngine;

namespace Source.Scripts.Components.UnityComponents
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private TriggerListener _triggerListener;

        private Entity _entity;
        public Action<Entity> OnEntityCollision;

        public void SetEntity(Entity entity)
        {
            _entity = entity;
        }

        private void OnEnable()
        {
            _triggerListener.OnTriggerEntered += CheckCollision;
        }

        private void OnDisable()
        {
            _triggerListener.OnTriggerEntered -= CheckCollision;
        }

        private void CheckCollision(GameObject obj)
        {
            if (!obj.TryGetComponent(out EntityView entityView)) return;

            OnEntityCollision?.Invoke(entityView._entity);
        }

    }
}