using StudentsManagerMW.Models;

namespace StudentsManagerMW.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents(int page, int pageSize, string orderBy, string sortOrder, string name);
        Student GetStudentById(int id);
        Student CreateStudent(Student student);
        Student UpdateStudent(int id, Student updatedStudent);
        bool DeleteStudent(int id);
    }
}
