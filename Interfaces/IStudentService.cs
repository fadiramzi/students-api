using StudentsAPIAuth.Models;

namespace StudentsAPIAuth.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        Student CreateStudent(Student student);
        Student UpdateStudent(int id, Student updatedStudent);
        bool DeleteStudent(int id);
    }
}
