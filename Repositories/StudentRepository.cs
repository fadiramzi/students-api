using Microsoft.EntityFrameworkCore;
using StudentsManagerMW.EFCore;
using StudentsManagerMW.Interfaces;
using StudentsManagerMW.Models;

namespace StudentsManagerMW.Repositories
{
    public class StudentRepository:CRUDRespository<Student>, IStudentRepository
    {
        private readonly AppDBContext _context;
        public StudentRepository(AppDBContext context):base(context)
        {
            _context = context;
        }
        // CRUD operations

        public async Task<IEnumerable<Student>> GetAll(int page, int pageSize, string sortBy, string sortOrder, string name)
        {
            
            IQueryable<Student> query = _context.Students;

            if (!string.IsNullOrEmpty(name))
            {
               query = query.Where(s => s.Name.Contains(name));
            }
            int skip = (page - 1) * pageSize;
            query = OrderBy(query, sortBy, sortOrder);
            return await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        private IQueryable<Student> OrderBy(IQueryable<Student> query, string sortBy, string sortOrder)
        {
            if (sortOrder == "asc")
            {
                switch (sortBy)
                {
                    case "name":
                        return query.OrderBy(s => s.Name);
                    case "id":
                        return query.OrderBy(s => s.Id);
                    default:
                        return query.OrderBy(s => s.Id);
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "name":
                        return query.OrderByDescending(s => s.Name);
                    case "id":
                        return query.OrderByDescending(s => s.Id);
                    default:
                        return query.OrderByDescending(s => s.Id);
                }
            }   
        }
        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        

    }
}
