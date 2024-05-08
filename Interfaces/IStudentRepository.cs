using StudentsManagerMW.Models;

namespace StudentsManagerMW.Interfaces
{
    public interface IStudentRepository:ICRUDRepository<Student>
    {
        public Task<IEnumerable<Student>> GetAll(int page, int pageSize, string sortBy, string sortOrder , string name);
    }
}
