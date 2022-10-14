using System;
using UnityEngine;

namespace Source.Scripts.Data
{
    [Serializable]
    public class MovementData
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation
        {
            get => rotation;

            set
            {
                rotation = value;

                if (Mathf.Abs(rotation) > 360) rotation += Mathf.Sign(rotation) * -360;
            }
        }


        private float rotation;

        public MovementData() { }
        public MovementData(MovementData movementData)
        {
            Position = movementData.Position;
            Velocity = movementData.Velocity;
            Rotation = movementData.Rotation;
        }

        public Vector2 Forward => new(-Mathf.Sin(Rotation * Mathf.Deg2Rad), Mathf.Cos(Rotation * Mathf.Deg2Rad));
    }
}