namespace Source.Scripts.Systems
{
    public abstract class BaseSystem
    {
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);
    }
}