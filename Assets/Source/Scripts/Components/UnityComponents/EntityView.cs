using System;
using Source.Scripts.Entities;
using UnityEngine;

namespace Source.Scripts.Components.UnityComponents
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private TriggerListener _triggerListener;

        public Entity Entity;
        public event Action<Entity> OnEntityCollision;

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
            if(!obj.TryGetComponent(out EntityView entityView)) return;
            
            OnEntityCollision?.Invoke(entityView.Entity);
        }

    }
}