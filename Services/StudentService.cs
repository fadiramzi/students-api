using StudentsManagerMW.Interfaces;
using StudentsManagerMW.Models;

namespace StudentsManagerMW.Services
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<IEnumerable<Student>> GetAllStudents(int page, int pageSize, string sortBy, string sortOrder , string name)
        { 
            return await _studentRepository.GetAll(page, pageSize,sortBy, sortOrder,name);
        }

        public Student GetStudentById(int id)
        {
            var student = _studentRepository.GetById(id);
            return student;
        }


        public Student CreateStudent(Student student)
        {
            _studentRepository.Add(student);
            return student;
        }

        public Student UpdateStudent(int id, Student updatedStudent)
        {
            var existingStudent = _studentRepository.GetById(id);
            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                _studentRepository.Update(existingStudent);
                return existingStudent;
            }
          
            return null;
        }

        public bool DeleteStudent(int id)
        {
            // Find and remove student by ID
            var existingStudent = _studentRepository.GetById(id);
            if (existingStudent != null)
            {
                 _studentRepository.Delete(existingStudent);
                return true;
            }
            return false;
        }   
    }
}
