using System;
using UnityEngine;

namespace Source.Scripts.Data
{
    [Serializable]
    public class MovementData
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;

        public MovementData(){}
        public MovementData(MovementData movementData)
        {
            Position = movementData.Position;
            Velocity = movementData.Velocity;
            Rotation = movementData.Rotation;
        }

        public Vector2 Forward => new(-Mathf.Sin(Rotation * Mathf.Deg2Rad), Mathf.Cos(Rotation * Mathf.Deg2Rad));
    }
}