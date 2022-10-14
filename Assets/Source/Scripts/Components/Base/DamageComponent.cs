using System;
using System.Collections.Generic;
using Source.Scripts.Entities;
using Source.Scripts.Enums;

namespace Source.Scripts.Components
{
    public class DamageComponent
    {
        private readonly List<EntityType> _ignoreTypes;
        private Action _afterHitCallbackCallback;

        public DamageComponent(List<EntityType> ignoreTypes, ref Action<Entity> hitCallback, Action afterHitCallback = null)
        {
            _ignoreTypes = ignoreTypes;
            hitCallback += CheckCollision;
            _afterHitCallbackCallback = afterHitCallback;
        }

        private void CheckCollision(Entity entity)
        {
            if (_ignoreTypes.Contains(entity.EntityType)) return;

            entity.Erase();
            _afterHitCallbackCallback?.Invoke();
            
        }


    }
}