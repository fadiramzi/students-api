using StudentsAPIAuth.EFCore;
using StudentsAPIAuth.Interfaces;
using StudentsAPIAuth.Models;

namespace StudentsAPIAuth.Repositories
{
    public class StudentRepository:CRUDRespository<Student>, IStudentRepository
    {
        private readonly AppDBContext _context;
        public StudentRepository(AppDBContext context):base(context)
        {
            _context = context;
        }
        // CRUD operations

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
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
