using Microsoft.EntityFrameworkCore;
using StudentsAPIAuth.Interfaces;

namespace StudentsAPIAuth.Repositories
{
    public class CRUDRespository<TEntity> : ICRUDRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public CRUDRespository(DbContext dbContext)
        {
            _context = dbContext;

        }
        public void Add(TEntity entity)
        {
           _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
           return _context.Set<TEntity>().ToList();

        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
