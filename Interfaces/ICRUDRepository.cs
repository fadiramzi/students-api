using StudentsManagerMW.Models;

namespace StudentsManagerMW.Interfaces
{
    public interface ICRUDRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        Task<IEnumerable<TEntity>> GetAll(int page, int pageSize);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
