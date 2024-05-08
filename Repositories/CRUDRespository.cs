using Microsoft.EntityFrameworkCore;
using StudentsManagerMW.Interfaces;

namespace StudentsManagerMW.Repositories
{
    public class CRUDRespository<TEntity> : ICRUDRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public CRUDRespository(DbContext dbContext)
        {
            _context = dbContext;

        }

        public async Task<IEnumerable<TEntity>> GetAll(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return await _context.Set<TEntity>()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

        }
        public void Add(TEntity entity)
        {
           _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
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
