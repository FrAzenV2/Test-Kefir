using Source.Scripts.Entities;

namespace Source.Scripts
{
    public interface IEntityUpdater
    {
        public void AddEntity(Entity entity);
        public void RemoveAllEntities();
    }
}