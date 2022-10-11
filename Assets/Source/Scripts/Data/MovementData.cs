using UnityEngine;

namespace Source.Scripts
{
    public struct MovementData
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;

        public Vector2 Forward => new(Mathf.Cos(Rotation * Mathf.Deg2Rad), Mathf.Sin(Rotation * Mathf.Deg2Rad));
    }
}