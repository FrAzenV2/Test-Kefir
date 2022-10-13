using System;
using System.Collections.Generic;
using Source.Scripts.Entities;
using Source.Scripts.Enums;

namespace Source.Scripts.Components
{
    public class DamageComponent
    {
        private readonly List<EntityType> _ignoreTypes;

        public DamageComponent(List<EntityType> ignoreTypes, Action<Entity> hitCallback)
        {
            _ignoreTypes = ignoreTypes;
            hitCallback += CheckCollision;
        }

        private void CheckCollision(Entity entity)
        {
            if (_ignoreTypes.Contains(entity.EntityType)) return;

            entity.Erase();
        }


    }
}